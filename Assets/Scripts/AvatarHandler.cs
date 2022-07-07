using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ReadyPlayerMe;
using System;
using Photon.Pun;
using StarterAssets;

public class AvatarHandler : MonoBehaviourPunCallbacks
{
	public PhotonView PV;
	public string avatarUrl;
	public GameObject Player;
	public Avatar PlayerAvatar;
	public AvatarLoader avatarLoader;
    public int OwnerActorNum;
	public GameObject Test;
    public string[] stringArray;
    string actor_S, AvatarURL_S;
    public Slider LoadingSlider;
    public GameObject PlayerNameTag;
    public GameObject LoadingCanvas;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
		if (PV.IsMine)
		{
            Debug.Log("First GetOwnerActorNumber");
            GetOwnerActorNumber();
            avatarUrl = AvatarGetter.Instance.Url;
            LoadAvatar(avatarUrl);
            string rpcString = BuildString();
            PV.RPC("ReceiveRPCAvatar", RpcTarget.OthersBuffered, rpcString);
        }
	}
 
    public void LoadAvatar(string URL)
    {
        Debug.Log("Load Avatar");
        avatarLoader = new AvatarLoader();
        avatarLoader.OnCompleted += AvatarLoadingCompleted;
        avatarLoader.OnFailed += AvatarLoadingFailed;
        avatarLoader.OnProgressChanged += AvatarLoadingProgressChanged;
        avatarLoader.LoadAvatar(URL);
    }

    private void AvatarLoadingCompleted(object sender, CompletionEventArgs args)
    {
        Debug.Log($"{args.Avatar.name} is imported!");
        Player = args.Avatar.gameObject;
        GetComponent<ThirdPersonController>().canPlayerMove = true;
        SetPlayer(gameObject);
        GameHandler.Instance.ActivateJoinButton();
        PlayerNameTag.SetActive(true);
        LoadingCanvas.SetActive(false);
    }

    [PunRPC]
    public void ReceiveRPCAvatar(string URL)
    {
        Debug.Log("Second GetOwnerActorNumber");
        GetOwnerActorNumber();
        stringSplit(URL);
        string str2 =  OwnerActorNum.ToString();
        if(CompareString(actor_S,str2))
        {
            //avatarLoader.LoadAvatar(AvatarURL_S);
            LoadAvatar(AvatarURL_S);
        }
    }

    public void stringSplit(string url)
    {
        stringArray = url.Split(',');
        actor_S = stringArray[0];
        AvatarURL_S = stringArray[1];
    }

    public bool CompareString(string str1,string str2)
    {
        Debug.Log(string.Equals(str1, str2));
        return string.Equals(str1, str2);
    }
    private void AvatarLoadingFailed(object sender, FailureEventArgs args)
    {
        Debug.LogError($"Failed with {args.Type}: {args.Message}");
    }
    private void AvatarLoadingProgressChanged(object sender, ProgressChangeEventArgs args)
    {
        //Debug.Log($"Progress: {args.Progress * 100}%");
        LoadingSlider.value = args.Progress;
    }

    public void SetPlayer(GameObject spawnedPlayer)
    {
        PlayerAvatar = Player.GetComponent<Animator>().avatar;
        Player.GetComponent<Animator>().enabled = false;
        Player.transform.SetParent(spawnedPlayer.transform);
        Player.transform.localPosition = Vector3.zero;
        Player.transform.localRotation = Quaternion.identity;
        Player.SetActive(true);
        spawnedPlayer.GetComponent<Animator>().avatar = PlayerAvatar;
    }

    public string BuildString()
    {
        return  OwnerActorNum +"," + avatarUrl;
    }

    public void GetOwnerActorNumber()
    {
        PV = GetComponent<PhotonView>();
        Debug.Log(PV.OwnerActorNr);
        OwnerActorNum = PV.OwnerActorNr;
    }
}
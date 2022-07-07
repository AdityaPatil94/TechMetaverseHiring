using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using StarterAssets;
using UnityEngine.UI;
public class WhiteBoardInputHandler : MonoBehaviour
{
    public TextMeshProUGUI WhiteBoardText;
    public TMP_InputField WhiteBoardInputField;
    public PhotonView PV;
    public GameObject InputCanvas;
    public TextMeshProUGUI[] test;
    private void Start()
    {
        //test = GameObject.Fin("WhiteBoaardText");
        test = GameObject.FindObjectsOfType<TextMeshProUGUI>();
        // Debug.Log(test.name);
        foreach (TextMeshProUGUI TMPtext in test)
        {
            Debug.LogWarning(TMPtext.tag);
            if (TMPtext.tag == "WhiteBoardText")
            {
                WhiteBoardText =  TMPtext;
            }
        }
        
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
        {
            InputCanvas.SetActive(false);
            WhiteBoardInputField.gameObject.SetActive(false);
        }
    }

    public void DisplayText()
    {
        Debug.Log(WhiteBoardInputField);
       
        if (PV.IsMine)
        {
            WhiteBoardText.text = WhiteBoardInputField.text;
            WhiteBoardInputField.text = "";
            PV.RPC("DisplayLocalText", RpcTarget.OthersBuffered, WhiteBoardText.text);
        }
    }

    [PunRPC]
    public void DisplayLocalText(string Messgae)
    {
        Debug.Log("RPC PUN"+ WhiteBoardInputField.text);
        WhiteBoardText.text = Messgae;
    }

    public void TogglePlayerMovement(bool canMove)
    {
        GameHandler.Instance.TogglePlayerMovement(canMove);
    }
}

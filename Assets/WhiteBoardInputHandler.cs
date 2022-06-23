using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
public class WhiteBoardInputHandler : MonoBehaviour
{
    public TextMeshProUGUI WhiteBoardText;
    public TMP_InputField WhiteBoardInputField;
    public PhotonView PV;
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
        if(!PV.IsMine)
        {
            Destroy(this.gameObject);
        }
    }

    public void DisplayText()
    {
        Debug.Log(WhiteBoardInputField);
        WhiteBoardText.text = WhiteBoardInputField.text;
        if (PV.IsMine)
        {
            PV.RPC("DisplayLocalText", RpcTarget.All);
        }
    }

    [PunRPC]
    public void DisplayLocalText()
    {
        Debug.Log("RPC PUN");
        WhiteBoardText.text = WhiteBoardInputField.text;
    }
}

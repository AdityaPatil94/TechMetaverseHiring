using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;
public class UIHandler : MonoBehaviour
{
    public float FadeTime = 0;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;
    public Vector2 PanelPosition;
    PhotonView pv;
    bool IsChatPanelActive;
    public void PanelFadeIn()
    {
        canvasGroup.alpha = 0;
        rectTransform.DOAnchorPos(new Vector2(0f, 0f), FadeTime, false).SetEase(Ease.Linear);
        canvasGroup.DOFade(1, FadeTime);
        if(!pv.IsMine)
        {
            pv.RPC("NetworkPanelFadeIn", RpcTarget.OthersBuffered);
        }
    }

    public void PanelFadeOut()
    {
        
        canvasGroup.alpha = 1;
        rectTransform.DOAnchorPos(PanelPosition, FadeTime, true).SetEase(Ease.Linear);
        canvasGroup.DOFade(0, FadeTime);
        //if (!pv.IsMine)
        //{
        //    pv.RPC("NetworkPanelFadeOut", RpcTarget.OthersBuffered);
        //}
    }
    
    [PunRPC]
    public void NetworkPanelFadeOut()
    {
        canvasGroup.alpha = 1;
        rectTransform.DOAnchorPos(PanelPosition, FadeTime, true).SetEase(Ease.Linear);
        canvasGroup.DOFade(0, FadeTime);
    }

    [PunRPC]
    public void NetworkPanelFadeIn()
    {
        Debug.Log("NetworkPanelFadeIn");
        canvasGroup.alpha = 0;
        rectTransform.DOAnchorPos(new Vector2(0f, 0f), FadeTime, false).SetEase(Ease.Linear);
        canvasGroup.DOFade(1, FadeTime);
    }

    public void ToggleChatPanel()
    {
        IsChatPanelActive = !IsChatPanelActive;
        if(IsChatPanelActive)
        {
            PanelFadeOut();
        }
        else
        {
            PanelFadeIn();
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        IsChatPanelActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

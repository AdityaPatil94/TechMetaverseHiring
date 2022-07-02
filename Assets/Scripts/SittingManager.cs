using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StarterAssets;

public class SittingManager : MonoBehaviour
{
    public Transform targetPosition;
    public void MakePlayerSit()
    {
        Debug.Log("On pointer Down Event");
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Player");
        
        foreach(GameObject go in characters )
        {
            PhotonView pv = go.GetComponent<PhotonView>();
            if(pv.IsMine)
            {
                if( !go.GetComponent<ThirdPersonController>().isPlayerSitting)
                {
                    go.transform.GetComponentInParent<Transform>().SetPositionAndRotation(targetPosition.position, targetPosition.rotation);
                    go.GetComponent<Animator>().SetBool("Sit", true);
                    go.GetComponent<ThirdPersonController>().CanMove(false);
                    return;
                }
                
            }
        }
    }
}

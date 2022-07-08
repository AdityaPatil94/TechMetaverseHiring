using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StarterAssets;

public class SittingManager : MonoBehaviour
{
    public Transform targetPosition;
    public bool isChairEmpty;
    public void MakePlayerSit()
    {
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in characters)
        {
            PhotonView pv = go.GetComponent<PhotonView>();
            if (pv.IsMine)
            {
                if (isChairEmpty)
                {
                    go.transform.GetComponentInParent<Transform>().SetPositionAndRotation(targetPosition.position, targetPosition.rotation);
                    go.GetComponent<Animator>().SetBool("Sit", true);
                    go.GetComponent<ThirdPersonController>().CanMove(false);
                    isChairEmpty = false;
                    return;
                }
            }
        }
    }

    [PunRPC]
    public void ChairStatus()
    {

    }
}

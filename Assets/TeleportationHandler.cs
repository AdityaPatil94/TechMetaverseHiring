using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationHandler : MonoBehaviour
{
    public GameObject Teleportation;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Teleportation.SetActive(false);
        }
    }
}

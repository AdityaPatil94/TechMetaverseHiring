using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BotHandler : MonoBehaviour
{
    public AudioSource K2Message;
    public NavMeshAgent BotNavMesh;
    public Transform BotDestination;

    private void Start()
    {
        //SetBotDestintion();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SetBotDestintion();
        }
    }
    public void SetBotDestintion()
    {
        BotNavMesh.SetDestination(BotDestination.position);
    }
    public void PlayAudio()
    {
        K2Message.Play();
    }

}

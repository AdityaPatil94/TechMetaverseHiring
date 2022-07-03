using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BotHandler : MonoBehaviour
{
    public AudioSource K2MsgAudioSource;
    public NavMeshAgent BotNavMesh;
    public Transform BotDestination;
    public AudioClip[] AudioClipList;

    public delegate void OnloopPointReached();
    public static event OnloopPointReached AudioloopPointReached;
    public delegate void OnAudioClipListOver();
    public static event OnAudioClipListOver AudioClipListOver;

    int count = 0;

    private void Awake()
    {
        VideoManager.VideoClipListOver += NextAudio;
        AudioloopPointReached += NextAudio;

        //SetBotDestintion();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SetBotDestintion();
        }

        if(K2MsgAudioSource.clip!=null)
        {
            if (K2MsgAudioSource.clip.length == K2MsgAudioSource.time)
            {
                Debug.Log("Audio clip ended");
                K2MsgAudioSource.Stop();
                K2MsgAudioSource.time = 0;
                AudioloopPointReached.Invoke();
            }
        }
        

    }
    public void SetBotDestintion()
    {
        BotNavMesh.SetDestination(BotDestination.position);
    }
    public void PlayAudio(int ClipNumber)
    {
        K2MsgAudioSource.clip = AudioClipList[ClipNumber];
        K2MsgAudioSource.Play();
    }

    public void NextAudio()
    {
        Debug.Log("Play Next Audio" + AudioClipList.Length);

        //audio clip count
        
        if(count < AudioClipList.Length)
        {
            PlayAudio(count);
            count++;
        }
        else
        {
            AudioClipListOver.Invoke();
        }
    }

}

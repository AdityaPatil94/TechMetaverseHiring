using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer CustomVideoPlayer;
    public VideoClip[] VideoClipList;
    public int count;
    public bool isMute = false;
    public Sprite[] AudioToggle;
    public Image VideoMuteImage;
    public AudioSource K2Message;
    // Start is called before the first frame update
    void Start()
    {
        CustomVideoPlayer.loopPointReached += EndPointReached;
        PlayVideo(count);
    }



    public void PlayVideo(int VideoNumber)
    {
        CustomVideoPlayer.clip = VideoClipList[VideoNumber];
        CustomVideoPlayer.Play();
        Debug.Log("Playing .." + CustomVideoPlayer.clip.name);
    }

    public void EndPointReached(VideoPlayer vp)
    {
        Debug.Log("Video Reached to end");
        count++;
        if (count < 2)
        {
            //count = 0;
            PlayVideo(count);
        }
        else
        {
            PlayAudio();
        }
        
    }

    public void PlayAudio()
    {
        K2Message.Play();
    }

    public void ToggleAudio()
    {
        isMute = !isMute;
        Debug.Log(isMute);
        CustomVideoPlayer.SetDirectAudioMute(0, isMute);
        if(!isMute)
        {
            VideoMuteImage.sprite = AudioToggle[0];
        }
        else
        {
            VideoMuteImage.sprite = AudioToggle[1];
        }
    }
}

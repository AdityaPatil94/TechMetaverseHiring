using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{

    public VideoPlayer CustomVideoPlayer;
    public VideoClip[] VideoClipList;
    public string VideoSourceLink;
    public int count;
    public bool isMute = false;
    public Sprite[] AudioToggle;
    public Image VideoMuteImage;
    public bool HasURLVideoPlyed;
    public delegate void OnVideoClipListOver();
    public static event OnVideoClipListOver VideoClipListOver;
    // Start is called before the first frame update
    void Start()
    {
        BotHandler.AudioClipListOver += AudioClipReachedEndPoint;
        CustomVideoPlayer.loopPointReached += EndPointReached;
        PlayVideo(count);
    }

    public void PlayVideo(int VideoNumber)
    {
        CustomVideoPlayer.clip = VideoClipList[VideoNumber];
        CustomVideoPlayer.Play();
        //Debug.Log("Playing .." + CustomVideoPlayer.clip.name);
    }

    public void EndPointReached(VideoPlayer vp)
    {
        count++;
        if (count < 2)
        {
            PlayVideo(count);
        }
        else if(!HasURLVideoPlyed)
        {
            HasURLVideoPlyed = true;
            VideoClipListOver.Invoke();
        }
    }
    
    public void AudioClipReachedEndPoint()
    {
        Debug.Log("Play video from URL");
        {
            CustomVideoPlayer.source = VideoSource.Url;
            CustomVideoPlayer.url = VideoSourceLink;
            CustomVideoPlayer.Play();
        }
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

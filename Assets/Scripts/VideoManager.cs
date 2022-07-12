using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using YoutubePlayer;
using Photon.Pun;

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
    public GameObject K2botPrefab;
    public Vector3 BotSpawnPosition;
    GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        temp = Instantiate(K2botPrefab, BotSpawnPosition, Quaternion.Euler(0,0,0));
        temp.SetActive(false);
        BotHandler.AudioClipListOver += AudioClipReachedEndPoint;
        CustomVideoPlayer.loopPointReached += EndPointReached;
        //PlayVideo(count);
        //AudioClipReachedEndPoint();
    }

    public void PlayVideo(int VideoNumber)
    {
        CustomVideoPlayer.clip = VideoClipList[VideoNumber];
        CustomVideoPlayer.Play();
        //Debug.Log("Playing .." + CustomVideoPlayer.clip.name);
    }

    public void StopVideo()
    {
        CustomVideoPlayer.Pause();
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
            temp.SetActive(true);
            HasURLVideoPlyed = true;
            VideoClipListOver.Invoke();
        }
    }

    async public void AudioClipReachedEndPoint()
    {
        Debug.Log("Play video from URL");
        {
            //CustomVideoPlayer.source = VideoSource.Url;
            //CustomVideoPlayer.url = VideoSourceLink;
            CustomVideoPlayer = GetComponent<VideoPlayer>();
            await CustomVideoPlayer.PlayYoutubeVideoAsync(VideoSourceLink);
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


    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(other.GetComponent<PhotonView>().IsMine)
            {
                PlayVideo(count);
            }
            Debug.Log("Play Video");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                StopVideo();
            }
                Debug.Log("Stop Video");
        }
    }
}

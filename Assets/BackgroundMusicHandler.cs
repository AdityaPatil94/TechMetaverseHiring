using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicHandler : MonoBehaviour
{
    public AudioClip[] BackgroundMusicList;
    public AudioSource BackgroundMusicAS;
    public static BackgroundMusicHandler Instance;
    private bool toggleMusic;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance!= null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( !BackgroundMusicAS.isPlaying)
        {
            NextRandomClip();
        }
    }

    public void PlayMusic(int ClipNumber)
    {
        BackgroundMusicAS.clip = BackgroundMusicList[ClipNumber];
        BackgroundMusicAS.Play();
    }

    public void PauseMusic()
    {
        BackgroundMusicAS.Pause();
    }

    public void Mute()
    {
        toggleMusic = !toggleMusic;
        BackgroundMusicAS.mute = toggleMusic;
    }

    public void StopMusic()
    {
        BackgroundMusicAS.Stop();
    }

    public void NextRandomClip()
    {
        int clipNumber = Random.Range(0,6);
        PlayMusic(clipNumber);
    }
}

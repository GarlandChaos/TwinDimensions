using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
    public static GameObject instance;
    bool playAmbient;
    AudioSource source;
    public AudioClip soundVictory;
    public AudioClip soundDefeat;
    public AudioClip soundMouseOver;
    public AudioClip soundMouseClick;
    [SerializeField]
    float volumeSFX;
    float volumeMusic;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this.gameObject;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        
        volumeSFX = 0.5f;
        volumeMusic = 0.5f;
        playAmbient = true;
        source = this.GetComponent<AudioSource>();
        source.volume = volumeMusic;
       
    }

    // Update is called once per frame
    void Update()
    {
        controls();
    }
    
    private void controls()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
            else
            {
                source.Play();
            }
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        if (level == 3 || level == 4)
        {
            playAmbient = false;
            source.Stop();
        }
        else
        {
            if (playAmbient == false)
            {
                playAmbient = true;
                source.Play();
            }

            if (level == 5)
            {
                source.PlayOneShot(soundDefeat);
            }
            if (level == 6)
            {
                source.PlayOneShot(soundVictory);
            }
        }
    }

    public void playMouseOver()
    {
        source.PlayOneShot(soundMouseOver);
    }

    public void playMouseClick()
    {
        source.PlayOneShot(soundMouseClick);
    }

    public void changeSourceLevel(float v)
    {
        source.volume = v;
        volumeMusic = v;
    }

    public void changeSFXLevel(float v)
    {
        volumeSFX = v;
    }

    public void setvolumeSFX(float a)
    {
        volumeSFX = a;
    }

    public float getvolumeSFX()
    {
        return volumeSFX;
    }

    public void setvolumeMusic(float a)
    {
        volumeMusic = a;
    }

    public float getvolumeMusic()
    {
        return volumeMusic;
    }
}

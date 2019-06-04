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

    // Start is called before the first frame update
    void Start()
    {
        instance = this.gameObject;
        DontDestroyOnLoad(this);
        playAmbient = true;
        source = this.GetComponent<AudioSource>();
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundController : MonoBehaviour
{
    AudioSource source;
    public static GameObject instance;
    public AudioClip soundDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        instance = this.gameObject;
        source = this.GetComponent<AudioSource>();
        source.volume = audioController.instance.GetComponent<audioController>().getvolumeMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playDestroy()
    {
        source.PlayOneShot(soundDestroyed, audioController.instance.GetComponent<audioController>().getvolumeSFX());
    }
}

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playDestroy()
    {
        source.PlayOneShot(soundDestroyed);
    }
}

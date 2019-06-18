using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioLevelInterface : MonoBehaviour
{
    List<GameObject> audioLevels;
    int nLevels = 10;
    float xOffset = 50.0f;
    [SerializeField]
    int currentLevel;

    [SerializeField]
    bool music;

    AudioSource audioSource;
    public AudioClip soundShoot;

    // Start is called before the first frame update
    void Start()
    {
        audioLevels = new List<GameObject>();
        Vector3 instPos = this.transform.position;
        instPos.x -= this.gameObject.GetComponent<RectTransform>().rect.width / 2;
        instPos.x += 30;

        for (int i = 0; i < nLevels; i++)
        {
            GameObject a = Instantiate(Resources.Load("audioLevel"), instPos, this.transform.rotation) as GameObject;
            a.transform.SetParent(this.transform);
            a.GetComponent<audioLevel>().setId(i + 1);

            instPos = a.transform.position;
            instPos.x += xOffset;

            audioLevels.Add(a);
        }

        if (music)
        {
            float val = audioController.instance.GetComponent<audioController>().getvolumeMusic() * 10f;
            currentLevel = (int)val;
            Debug.Log("VOLUME: " + (int)val);
        }
        else
        {
            float val = audioController.instance.GetComponent<audioController>().getvolumeSFX() * 10f;
            currentLevel = (int)val;
            //Debug.Log("VOLUME: " + (int)val);
        }


        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        checkLevels();
    }

    void checkLevels()
    {
        for (int i = 0; i < nLevels; i++)
        {
            if (i < currentLevel)
            {
                audioLevels[i].GetComponent<audioLevel>().setOn();

            }
            else
            {
                audioLevels[i].GetComponent<audioLevel>().setOff();

            }
        }
    }

    public void setCurrentLevel(int a)
    {
        currentLevel = a;
    }

    public void setVolume()
    {
        float volume = (float)(currentLevel) / 10.0f;
        //Debug.Log("volume: " + volume + " currentLevel: " + currentLevel);

        if (music)
        {
            audioController.instance.GetComponent<audioController>().changeSourceLevel(volume);
        }
        else
        {
            audioController.instance.GetComponent<audioController>().changeSFXLevel(volume);
            audioSource.PlayOneShot(soundShoot, audioController.instance.GetComponent<audioController>().getvolumeSFX());
        }
    }
}

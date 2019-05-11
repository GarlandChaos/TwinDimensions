using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public static GameObject instance;
    public int nWave;
    public int nEnemies;
    Vector3 instPos;
    Vector3 worldPos;
    float timeBetween;
    float time;
    public Text waveTxt;
    public bool stopWaves;
    public GameObject enemy1;
    public GameObject enemy2;

    // Start is called before the first frame update
    void Start()
    {
        instance = this.gameObject;
        nWave = 0;
        nEnemies = 0;
        timeBetween = 5.0f;
        time = 0.0f;
        stopWaves = false;
        //waveTxt = GameObject.FindGameObjectWithTag("healthText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        checkWave();
    }

    public void removeEnemy()
    {
        nEnemies--;
    }

    void checkWave()
    {
        if(!stopWaves)
        {
            waveTxt.text = "Wave: " + nWave.ToString();
        }
        else
        {
            waveTxt.text = "FINISHED! WAITING PLAYER 2";
        }

        if (nEnemies <= 0)
        {
            time += Time.deltaTime;

            if(time > timeBetween)
            {
                time = 0;
                if (!stopWaves)
                {
                    
                    nWave++;
                    if(nWave <= GameManager.instance.GetComponent<GameManager>().getMaxWaves())
                    {
                        spawnWave();

                    }
                    
                }
                else
                {
                    Debug.Log("Stopped waves!");
                }
                
            }
        }
    }

    void spawnWave()
    {
        int wave = Random.Range(1, 4);
        Debug.Log("wave number: " + wave);

        nEnemies = 0;

        if(nWave != GameManager.instance.GetComponent<GameManager>().getMaxWaves())
        {
            switch (wave)
            {
                case 1:
                    instPos = this.transform.position;
                    worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 3, Camera.main.pixelHeight, 100));
                    instPos.x += worldPos.x;
                    Instantiate(Resources.Load("enemy1"), instPos, enemy1.transform.rotation);
                    nEnemies++;

                    instPos = this.transform.position;
                    worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - Camera.main.pixelWidth / 3, Camera.main.pixelHeight, 100));
                    instPos.x += worldPos.x;
                    Instantiate(Resources.Load("enemy1"), instPos, enemy1.transform.rotation);
                    nEnemies++;

                    break;
                case 2:
                    instPos = this.transform.position;
                    worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 3, Camera.main.pixelHeight, 100));
                    instPos.x += worldPos.x;
                    Instantiate(Resources.Load("enemy1"), instPos, enemy1.transform.rotation);
                    nEnemies++;

                    instPos = this.transform.position;
                    worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - Camera.main.pixelWidth / 3, Camera.main.pixelHeight, 100));
                    instPos.x += worldPos.x;
                    Instantiate(Resources.Load("enemy2"), instPos, enemy2.transform.rotation);
                    nEnemies++;
                    break;
                case 3:
                    instPos = this.transform.position;
                    worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 4, Camera.main.pixelHeight, 100));
                    instPos.x += worldPos.x;
                    Instantiate(Resources.Load("enemy1"), instPos, enemy1.transform.rotation);
                    nEnemies++;

                    instPos = this.transform.position;
                    worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - Camera.main.pixelWidth / 4, Camera.main.pixelHeight, 100));
                    instPos.x += worldPos.x;
                    Instantiate(Resources.Load("enemy2"), instPos, enemy2.transform.rotation);
                    nEnemies++;

                    instPos = this.transform.position;
                    worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight, 100));
                    instPos.x += worldPos.x;
                    Instantiate(Resources.Load("enemy1"), instPos, enemy1.transform.rotation);
                    nEnemies++;
                    break;
                default:

                    break;
            }
        }
        else
        {
            //spawna o boss

            instPos = this.transform.position;
            worldPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight, 100));
            instPos.x += worldPos.x;
            instPos.y = worldPos.y - 10;
            Instantiate(Resources.Load("boss"), instPos, this.transform.rotation);
            nEnemies++;
            Debug.Log("SPAWNED BOSS!!!");
            GameObject bg = GameObject.Find("Background");
            bg.GetComponent<backgroundMove>().scrollSpeed = 0.5f;
        }
        
    }

    public void setnWave(int nWave)
    {
        this.nWave = nWave;
    }

    public int getnWave()
    {
        return nWave;
    }

    public void setStopWaves(bool stopWaves)
    {
        this.stopWaves = stopWaves;
    }

    public bool getStopWaves()
    {
        return stopWaves;
    }
}

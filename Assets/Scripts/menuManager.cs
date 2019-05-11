using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    public static GameObject instance;
    public GameObject pauseView;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this.gameObject;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        controls();
        //if(pauseView == null)
        //{
        //    pauseView = GameObject.FindGameObjectWithTag("pauseView");
        //}
    }

    public void newGame()
    {
        SceneManager.LoadScene("ConnectGame");
    }

    public void configuration()
    {
        SceneManager.LoadScene("Configuration");
    }

    public void credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    void controls()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Credits")
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        if(scene.name == "MainGame" || scene.name == "MainGameB")
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                pauseEvent();
            }
        }
    }


    public void pauseEvent()
    {
        Debug.Log("EVENT 7");
        byte evCode = 7; // Custom Event 0
        object[] content = new object[] { 1 }; // Array contains the target position and the IDs of the selected units
        bool reliable = true;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(evCode, content, reliable, raiseEventOptions);
    }

    public void OnEvent(byte eventCode, object content, int senderId)
    {
        // Do something
        if (eventCode == 7) //transferência de vida
        {
       
            //object[] data = (object[])content;

            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pauseView.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                pauseView.SetActive(true);
            }

        }
        
    }

    public void OnEnable()
    {
        PhotonNetwork.OnEventCall += OnEvent;
    }

    public void OnDisable()
    {
        PhotonNetwork.OnEventCall -= OnEvent;
    }
}

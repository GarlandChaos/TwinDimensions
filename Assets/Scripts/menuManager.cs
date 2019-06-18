using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.EventSystems;

public class menuManager : MonoBehaviour
{
    public static GameObject instance;
    public GameObject pauseView;
    public bool isOver = false;

    public Texture2D cursorTexture; //menus
    public Texture2D cursorTexture2; //in-game
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    bool cursorGame;

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

        cursorGame = false;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
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
        SceneManager.LoadScene("Configurations");
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
        if (scene.name == "Credits" || scene.name == "Configurations")
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        if(scene.name == "MainGame" || scene.name == "MainGameB")
        {
            //if (cursorGame)
            //{
                //cursorGame = false;
            //}
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                pauseEvent();
            }
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 3 || level == 4)
        {
            Cursor.SetCursor(cursorTexture2, hotSpot, cursorMode);
        }
        else
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
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

    //private void OnMouseEnter()
    //{
    //    Debug.Log("Mouse enter");
    //    isOver = true;
    //}

    //private void OnMouseExit()
    //{
    //    Debug.Log("Mouse exited");
    //    isOver = false;
    //}

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    Debug.Log("Mouse enter");
    //    isOver = true;
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    Debug.Log("Mouse exit");
    //    isOver = false;
    //}

    public void OnEnable()
    {
        PhotonNetwork.OnEventCall += OnEvent;
    }

    public void OnDisable()
    {
        PhotonNetwork.OnEventCall -= OnEvent;
    }
}

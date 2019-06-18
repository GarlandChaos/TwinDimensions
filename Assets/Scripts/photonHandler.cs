using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class photonHandler : MonoBehaviour {

    public photonButtons photonB;

    public GameObject mainPlayer;

    public static GameObject instance;

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this.gameObject;
        }
        else
        {
            Destroy(this.gameObject);
        }

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public void createNewRoom()
    {
        PhotonNetwork.CreateRoom(photonB.createRoomInput.text, new RoomOptions() { MaxPlayers = 2 }, null);
    }

    public void joinOrCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        //PhotonNetwork.JoinOrCreateRoom(photonB.joinRoomInput.text, roomOptions, TypedLobby.Default);
        PhotonNetwork.JoinOrCreateRoom("pMultijogador", roomOptions, TypedLobby.Default);
    }

    public void moveScene()
    {
        //if (PhotonNetwork.isMasterClient)
        //{
        //    PhotonNetwork.DestroyAll();

        //}

        if (PhotonNetwork.room.PlayerCount == 1)
        {
            GameManager.instance.GetComponent<GameManager>().gameSceneA = true;
            //PhotonNetwork.LoadLevel("MainGame");
            PhotonNetwork.LoadLevel("Waiting");

        }
        else if (PhotonNetwork.room.PlayerCount == 2)
        {
            GameManager.instance.GetComponent<GameManager>().gameSceneB = true;
            //PhotonNetwork.LoadLevel("MainGameB");
            //PhotonNetwork.LoadLevel("Waiting");

        }
    }

    public void OnJoinedRoom()
    {
        moveScene();
        Debug.Log("We are connected to the room.");
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //if (PhotonNetwork.isMasterClient)
        //{
        //    PhotonNetwork.DestroyAll();

        //}
        if (scene.name == "MainGame")
        {
            string n = mainPlayer.name + "1";
            spawnPlayer(n);
            //PhotonView[] pvs =  UnityEngine.Object.FindObjectsOfType<PhotonView>();
            //for(int i = 0; i < pvs.Length; i++)
            //{
            //    Debug.Log("i: " + i);
            //    PhotonPlayer pl = pvs[i].owner;
            //    if(pl != null)
            //    {
            //        GameObject plGameObj = pvs[i].gameObject;
            //        plGameObj.GetComponent<playerMove>().player2HP.SetActive(true);
            //        plGameObj.GetComponent<playerMove>().waitingP2.gameObject.SetActive(false);
            //    }
            //}
        }
        if(scene.name == "MainGameB")
        {
            string n = mainPlayer.name + "2";
            spawnPlayer(n);
        }
        if (scene.name == "GameOver")
        {
            //spawnPlayer();
            //PhotonView[] pvs =  UnityEngine.Object.FindObjectsOfType<PhotonView>();
            //for(int i = 0; i < pvs.Length; i++)
            //{
            //    Debug.Log("i: " + i);
            //    PhotonPlayer pl = pvs[i].owner;
            //    if(pl != null)
            //    {
            //        GameObject plGameObj = pvs[i].gameObject;
            //        plGameObj.GetComponent<playerMove>().player2HP.SetActive(true);
            //        plGameObj.GetComponent<playerMove>().waitingP2.gameObject.SetActive(false);
            //    }
            //}
        }
    }

    private void spawnPlayer(string name)
    {
        //PhotonNetwork.InstantiateSceneObject(mainPlayer.name, mainPlayer.transform.position, mainPlayer.transform.rotation, 0);
        PhotonNetwork.Instantiate(mainPlayer.name, mainPlayer.transform.position, mainPlayer.transform.rotation, 0);
        Debug.Log(name + " entered");
        //mainPlayer.GetComponent<playerMove>().player2HP.SetActive(true);
        //GameObject.FindGameObjectWithTag("waitingPlayer2").SetActive(false);
    }

    
    public void restartEvent()
    {
        byte evCode = 0; // Custom Event 0
        object[] content = new object[] { 1 }; // Array contains the target position and the IDs of the selected units
        bool reliable = true;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(evCode, content, reliable, raiseEventOptions);
    }

    public void mainMenuEvent()
    {
        byte evCode = 1; // Custom Event 1
        object[] content = new object[] { 1 }; // Array contains the target position and the IDs of the selected units
        bool reliable = true;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(evCode, content, reliable, raiseEventOptions);
    }

    public void OnEvent(byte eventCode, object content, int senderId)
    {
        // Do something
        if (eventCode == 0)
        {
            //object[] data = (object[])content;

            if (GameManager.instance.GetComponent<GameManager>().gameSceneA)
            {
                PhotonNetwork.LoadLevel("MainGame");
            }
            else if (GameManager.instance.GetComponent<GameManager>().gameSceneB)
            {
                PhotonNetwork.LoadLevel("MainGameB");
            }
        }
        if (eventCode == 1)
        {
            //object[] data = (object[])content;

            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
            PhotonNetwork.DestroyAll();
            Debug.Log("DISCONNECTING / NUMBER OF PLAYER IS: " + PhotonNetwork.room.PlayerCount);
            GameManager.instance.GetComponent<GameManager>().waiting = true;
            SceneManager.LoadScene("MainMenu");
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

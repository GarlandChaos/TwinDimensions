using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class photonButtons : MonoBehaviour {

    public photonHandler pHandler;

    public menuLogic mLogic;

    public InputField createRoomInput, joinRoomInput;

    //private void Awake()
    //{
    //    DontDestroyOnLoad(this.transform);

    //}

    public void onClickCreateRoom()
    {
        //if(createRoomInput.text.Length >= 1)
        //    PhotonNetwork.CreateRoom(createRoomInput.text, new RoomOptions() { MaxPlayers = 2 }, null);

        pHandler.createNewRoom();
    }	

    public void onClickJoinRoom()
    {
        //RoomOptions roomOptions = new RoomOptions();
        //roomOptions.MaxPlayers = 2;
        //PhotonNetwork.JoinOrCreateRoom(joinRoomInput.text, roomOptions, TypedLobby.Default);
        ////PhotonNetwork.JoinRoom(joinRoomInput.text);

        pHandler.joinOrCreateRoom();
    }

    public void restartScene() //rever essa função, o pHandler que deve fazer isso
    {
        //GameManager.instance.GetComponent<GameManager>().waiting = true;
        //PhotonNetwork.LoadLevel("Waiting");

        //if (GameManager.instance.GetComponent<GameManager>().gameSceneA)
        //{
        //    PhotonNetwork.LoadLevel("MainGame");
        //}
        //else if (GameManager.instance.GetComponent<GameManager>().gameSceneB)
        //{
        //    PhotonNetwork.LoadLevel("MainGameB");
        //}

        photonHandler.instance.GetComponent<photonHandler>().restartEvent();

    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void loadMainMenu()
    {
        photonHandler.instance.GetComponent<photonHandler>().mainMenuEvent();
    }

    //public void OnJoinedRoom()
    //{
    //    //mLogic.disableMenuUI();
    //    pHandler.moveScene();
    //    Debug.Log("We are connected to the room.");
    //}
}

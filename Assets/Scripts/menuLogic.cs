using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuLogic : MonoBehaviour {

    //public GameObject connectedMenu;

    public void disableMenuUI()
    {
        //connectedMenu.SetActive(false);
        PhotonNetwork.LoadLevel("MainGame");
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

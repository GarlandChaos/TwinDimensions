using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuLogic : MonoBehaviour {

    //public GameObject connectedMenu;

    public void disableMenuUI()
    {
        //connectedMenu.SetActive(false);
        PhotonNetwork.LoadLevel("MainGame");
    }
}

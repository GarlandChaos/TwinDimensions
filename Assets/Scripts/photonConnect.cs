using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class photonConnect : MonoBehaviour {

    public string versionName = "0.1";
    public GameObject sectionView1, sectionView2, sectionView3;

    private void Awake()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(versionName);

            Debug.Log("Connecting to Photon...");
        }
        
    }

    private void Update()
    {
        if (PhotonNetwork.connected)
        {
            sectionView1.SetActive(false);
            sectionView2.SetActive(true);
        }

    }

    private void OnConnectedToMaster()
    {

        PhotonNetwork.JoinLobby(TypedLobby.Default);

        Debug.Log("We are connected to master.");
    }

    private void OnJoinedLobby()
    {
        sectionView1.SetActive(false);
        sectionView2.SetActive(true);

        Debug.Log("On joined lobby.");
    }

    private void OnDisconnectedFromPhoton()
    {
        if (sectionView1.activeInHierarchy)
            sectionView1.SetActive(false);

        if (sectionView2.activeInHierarchy)
            sectionView2.SetActive(false);

        sectionView3.SetActive(true);

        Debug.Log("Disconnected from Photon services.");
    }

    //private void OnFailedToConnectToPhoton()
    //{

    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameObject instance;
    bool gameOver;
    bool gameWin;
    int maxWaves;
    bool player1Finish;
    bool player2Finish;
    public bool gameSceneA;
    public bool gameSceneB;
    public Text nPlayers;
    public bool waiting;

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

        gameOver = false;
        gameWin = false;
        player1Finish = false;
        player2Finish = false;
        maxWaves = 3;
        //maxWaves = 1;
        gameSceneA = false;
        gameSceneB = false;
        waiting = true;
    }

    // Update is called once per frame
    void Update()
    {
        checkGame();
        //Debug.Log("NUMBER OF PLAYERS: " + PhotonNetwork.room.PlayerCount + " at: " + PhotonNetwork.room.Name);
        nPlayers.text = "Number of players: " + PhotonNetwork.room.PlayerCount.ToString();
    }

    void checkGame()
    {
        if (waiting)
        {
            if (PhotonNetwork.room.PlayerCount == 2)
            {
                waiting = false;

                if (gameSceneA)
                {
                    PhotonNetwork.LoadLevel("MainGame");
                }
                else if (gameSceneB)
                {
                    PhotonNetwork.LoadLevel("MainGameB");
                }
            }
        }

        if (gameOver)
        {
            Debug.Log("gameOver: " + gameOver);
            gameOver = false;

            PhotonNetwork.LoadLevel("GameOver");
        }

        if (gameWin)
        {
            Debug.Log("gameWin: " + gameWin);
            gameWin = false;
            PhotonNetwork.LoadLevel("GameWin");

        }
    }

    public void setGameOver(bool gameOver)
    {
        this.gameOver = gameOver;
    }

    public bool getGameOver()
    {
        return gameOver;
    }

    public void setGameWin(bool gameWin)
    {
        this.gameWin = gameWin;
    }

    public bool getGameWin()
    {
        return gameWin;
    }

    public void setMaxWaves(int maxWaves)
    {
        this.maxWaves = maxWaves;
    }

    public int getMaxWaves()
    {
        return maxWaves;
    }

    public void setPlayer1Finish(bool player1Finish)
    {
        this.player1Finish = player1Finish;
    }

    public bool getplayer1Finish()
    {
        return player1Finish;
    }

    public void setPlayer2Finish(bool player2Finish)
    {
        this.player2Finish = player2Finish;
    }

    public bool getplayer2Finish()
    {
        return player2Finish;
    }
}

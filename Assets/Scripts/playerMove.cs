using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class playerMove : Photon.PunBehaviour, IPunObservable {

    public bool devTesting = false; //não esquecer de trocar para false!!!

    GameObject sceneCam;

    public float health;
    float maxHealth;

    public float health2;
    float maxHealth2;

    public Image healthImage;
    //Image healthImage2;

    public Text healthText;
    //Text healthText2;

    GameObject player2HP;
    Text waitingP2;

    public float hpEnvia;
    public float hpRecebe;

    public float special;
    Image specialImage;

    public float specialEnvia;
    public float specialRecebe;

    public bool takeDamage;

    //public Text hpEnviaTxt;
    //public Text hpRecebeTxt;

    float time;
    float timeShoot;

    public float shootRate;
    public bool P1end;
    public bool P2end;

    public bool gameIsOver;

    private void Start()
    {
        //PhotonView.DontDestroyOnLoad(this);

        if (photonView.isMine)
        {
            Transform shipTransf = GameObject.FindGameObjectWithTag("playerShip").transform;
            transform.SetParent(shipTransf);

            healthImage = GameObject.FindGameObjectWithTag("player1_hpFill").GetComponent<Image>();
            healthText = GameObject.FindGameObjectWithTag("healthText").GetComponent<Text>();

            specialImage = GameObject.FindGameObjectWithTag("player_specialFill").GetComponent<Image>();

        }
        else
        {
            healthImage = GameObject.FindGameObjectWithTag("player2_hpFill").GetComponent<Image>();
            healthText = GameObject.FindGameObjectWithTag("healthText2").GetComponent<Text>();
        }

        maxHealth = 1.0f;
        health = maxHealth;

        maxHealth2 = 1.0f;
        health2 = maxHealth2;


        //hpEnviaTxt = GameObject.FindGameObjectWithTag("hpEnviaTxt").GetComponent<Text>();
        //hpRecebeTxt = GameObject.FindGameObjectWithTag("hpRecebeTxt").GetComponent<Text>();

        //player2HP = GameObject.FindGameObjectWithTag("player2_hp") as GameObject;
        //player2HP.SetActive(false);

        hpEnvia = 0;
        hpRecebe = 0;

        specialEnvia = 0;
        specialRecebe = 0;

        time = 0.0f;
        timeShoot = 0.0f;

        takeDamage = true; //trocar para true!

        shootRate = 0.05f;

        P1end = false;
        P2end = false;

        gameIsOver = false;
    }
 
    private void Update()
    {
        //if (!devTesting)
        //{
        //    //move();
        //    //checkInput();
        //    //if (PhotonNetwork.room.PlayerCount == 2)
        //    //{

        //    //    Debug.Log("THERE IS 2 PLAYERS");
        //    //    //player2HP.SetActive(true);
        //    //    waitingP2.gameObject.SetActive(false);
        //    //}

        //    //checkInput();
        //    //receiveLife();
        //    if (photonView.isMine)
        //    {
        //        //checkInput();
        //    }
        //    else
        //    {
        //        //smoothNetMovement();
        //    }
        //    //else
        //    //    smoothNetMovement();
        //}
        //else
        //{
        //    checkInput();
        //}

        if (photonView.isMine)
        {
            checkInput();
            //receiveLife();
            //checkSpecial();
            checkLife();
            checkWave();
        }
        updateText();



    }

    private void updateText()
    {
        Image hp2 = GameObject.FindGameObjectWithTag("player2_hpFill").GetComponent<Image>();
        health2 = hp2.fillAmount;

        //specialImage.fillAmount = special;

        //if (photonView.isMine)
        //{
        //    healthImage.fillAmount = health;

        //    if (health > 0.15f)
        //    {
        //        healthText.text = health.ToString();
        //    }
        //    else
        //    {
        //        healthText.text = "LIMIT REACHED";
        //    }

        //}
        //else
        //{
        healthImage.fillAmount = health;

        if (health > 0.15f)
        {
            healthText.text = health.ToString();
        }
        else
        {
            healthText.text = "LIMIT REACHED";
        }
        //}

        if (photonView.isMine)
        {
            specialImage.fillAmount = special;
        }

        
        

    }

    private void checkInput()
    {
        timeShoot += Time.deltaTime * 0.2f;

        if (Input.GetKeyUp(KeyCode.Alpha0)) //reduz próprio HP, retirar na build final
        {
            health -= 0.1f;
        }
        if (Input.GetKeyUp(KeyCode.Alpha9)) //alterna entre levar ou não dano, retirar na build final
        {
            if (takeDamage)
            {
                takeDamage = false;
            }
            else
            {
                takeDamage = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)) //transferir vida, espaço
        {
            transferLife();
            Debug.Log("player " + photonView.viewID.ToString() + " dispared transferLife");
        }
        if (Input.GetKeyUp(KeyCode.Mouse1)) //usar especial, clique direito
        {
            useSpecial();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0)) //atirar, clique esquerdo
        {
            shoot();
        }


    }

    void shoot()
    {
        if (timeShoot > shootRate)
        {
            Debug.Log("shooting!");
            Vector3 instPos = this.transform.position;
            instPos.y += this.transform.localScale.y + 13.0f;
            Instantiate(Resources.Load("playerBullet"), instPos, this.transform.rotation);
            timeShoot = 0.0f;
        }
    }

    void useSpecial()
    {

    }

    public void checkWave()
    {
        int waves = WaveManager.instance.GetComponent<WaveManager>().getnWave();
        int maxWaves = GameManager.instance.GetComponent<GameManager>().getMaxWaves();

        if(waves > maxWaves && !P1end)
        {
            P1end = true;
            //photonView.RPC("playerEnded", PhotonTargets.OthersBuffered, P1end);
            p2EndEvent();
            WaveManager.instance.GetComponent<WaveManager>().setStopWaves(true);

            //if(GameManager.instance.GetComponent<GameManager>().getplayer1Finish() == false)
            //{
            //    GameManager.instance.GetComponent<GameManager>().setPlayer1Finish(true);
            //}
            //else
            //{
            //    GameManager.instance.GetComponent<GameManager>().setPlayer2Finish(true);
            //}
        }
        if (P1end && P2end)
        {
            GameManager.instance.GetComponent<GameManager>().setGameWin(true);
        }
    }

    //public void collisionHP()
    //{
    //    //photonView.RPC("collectibleHpEffect", PhotonTargets.OthersBuffered, 0.5f);
    //    collectibleHPEvent();
    //}

    public void collisionSpecial()
    {
        photonView.RPC("collectibleSpecialEffect", PhotonTargets.OthersBuffered, 0.5f); //MUDAR
    }

    public void receiveDamage()
    {
        if (takeDamage)
        {
            health -= 0.1f;
            //healthImage.fillAmount = health;
            //StartCoroutine(flashColor());
        }
    }


    void checkLife()
    {

        if (health <= 0.0f)
        {
            //Debug.Log("gameOver before: " + GameManager.instance.GetComponent<GameManager>().getGameOver());
            //gameIsOver = true;
            //photonView.RPC("gameisOver", PhotonTargets.OthersBuffered, gameIsOver); //MUDAR
            gameOverEvent();
            //Debug.Log("gameOver after: " + GameManager.instance.GetComponent<GameManager>().getGameOver());
        }

        if (gameIsOver)
        {
            GameManager.instance.GetComponent<GameManager>().setGameOver(true);
        }
    }

    //void receiveLife()
    //{
    //    if(hpRecebe > 0)
    //    {

    //        //time += Time.deltaTime * 0.2f;
    //        //if (time > 1.0f)
    //        //{
    //        //    time = 0.0f;

    //        //    if (health > 1.0f)
    //        //    {
    //        //        health = 1.0f;
    //        //    }
    //        //    hpRecebe = 0;
    //        //}
    //        float hpRestored = health + hpRecebe;
    //        //health = Mathf.Lerp(health, hpRestored, time);
    //        health += hpRecebe;
    //        if(health > 1.0f)
    //        {
    //            health = 1.0f;
    //        }
    //        hpRecebe = 0;
            

    //    }

    //}

    void checkSpecial()
    {
        if (specialRecebe > 0)
        {

            time += Time.deltaTime * 0.2f;
            if (time > 1.0f)
            {
                time = 0.0f;

                if (special > 1.0f)
                {
                    special = 1.0f;
                }
                specialImage.fillAmount = special;
                specialRecebe = 0;
            }
            float specialRestored = special + specialRecebe;
            special = Mathf.Lerp(special, specialRestored, time);
            specialImage.fillAmount = special;
            //health += hpRecebe;

        }
    }

    private void transferLife()
    {
        //hpEnvia = 1.0f - health2;
        //float hpLoss = health - hpEnvia;

        //if(hpLoss >= 0.15f)
        //{
        //    health = hpLoss;
        //}
        //else
        //{
        //    hpEnvia = health - 0.15f;
        //    health = 0.15f;
        //}

        hpEnvia = 0.1f;
        hpEnvia = 1 - health2;
        float hp1 = health - hpEnvia;
        if(hp1 > (15/100))
        {
            health = hp1;
            Debug.Log("MAIOR que 15/100! " + hp1);
        }
        else
        {
            hpEnvia = 0;
            Debug.Log("MENOR que 15/100...");
        }

        //photonView.RPC("SendHP", PhotonTargets.Others, hpEnvia);
        receiveHPEvent(hpEnvia);
        hpEnvia = 0;

            //healthImage2.fillAmount = Mathf.Lerp(healthImage2.fillAmount, 1.0f, Time.deltaTime);
            //healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, 1.0f, Time.deltaTime * 5f);
            //Debug.Log(Mathf.Lerp(healthImage.fillAmount, 1.0f, Time.deltaTime));
        //}
        //else
        //{
        //    healthImage2.fillAmount += healthImage2.fillAmount - healthImage.fillAmount;
        //    //healthImage2.fillAmount = Mathf.Lerp(healthImage2.fillAmount, 1.0f, Time.deltaTime);
        //    //healthImage.fillAmount = Mathf.Lerp(healthImage.fillAmount, 1.0f, Time.deltaTime * 5f);
        //    //Debug.Log(Mathf.Lerp(healthImage.fillAmount, 1.0f, Time.deltaTime));


        //}
    }
    
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            //envia dados para outro jogador
            //stream.SendNext(transform.position);

            stream.SendNext(health);
        }
        else
        {
            //recebe dados de outro jogador
            //selfPos = (Vector3)stream.ReceiveNext();
            this.health = (float)stream.ReceiveNext();
        }
    }

    //[PunRPC]
    //void SendHP(float a)
    //{
    //    //hpRecebe = a;
    //    //Debug.Log("a :" + hpRecebe);
    //    Debug.Log("RECEBEU: " + a);

    //    //if (!photonView.isMine)
    //    //{
    //        float newHealth = health + a;
    //        Debug.Log("newHealth: " + newHealth);
    //        Debug.Log("viewID: " + photonView.viewID);

    //        if (newHealth > 1)
    //        {
    //            health = 1;
    //            Debug.Log("health + a: " + health);

    //        }
    //        else
    //            {
    //                health = newHealth;
    //                Debug.Log("health + a: " + health);

    //        }
    //    //}

    //}

    //[PunRPC]
    //void collectibleHpEffect(float a)
    //{
    //    health = a; //MUDAR CASO NÃO USE MAIS A VARIÁVEL hpRecebe!!!
    //    Debug.Log("a :" + health);
    //}

    //[PunRPC]
    //void collectibleSpecialEffect(float a)
    //{
    //    specialRecebe = a;
    //    Debug.Log("a :" + specialRecebe);
    //}

    //[PunRPC]
    //void playerEnded(bool a)
    //{
    //    P2end = a;
    //    Debug.Log("a :" + P2end);
    //}

    //[PunRPC]
    //void gameisOver(bool a)
    //{
    //    gameIsOver = a;
    //    Debug.Log("a :" + gameIsOver);
    //}

    public void receiveHPEvent(float h)
    {
        byte evCode = 2; // Custom Event 0
        object[] content = new object[] { h }; // Array contains the target position and the IDs of the selected units
        bool reliable = true;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(evCode, content, reliable, raiseEventOptions);
    }

    public void collectibleHPEvent()
    {
        Debug.Log("EVENT 3");
        byte evCode = 3; // Custom Event 0
        object[] content = new object[] { 0.5f }; // Array contains the target position and the IDs of the selected units
        bool reliable = true;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(evCode, content, reliable, raiseEventOptions);
    }

    public void collectibleSpecialEvent()
    {
        Debug.Log("EVENT 4");
        byte evCode = 4; // Custom Event 0
        object[] content = new object[] { 0.5f }; // Array contains the target position and the IDs of the selected units
        bool reliable = true;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(evCode, content, reliable, raiseEventOptions);
    }

    public void gameOverEvent()
    {
        Debug.Log("EVENT 5");
        byte evCode = 5; // Custom Event 0
        object[] content = new object[] { true }; // Array contains the target position and the IDs of the selected units
        bool reliable = true;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(evCode, content, reliable, raiseEventOptions);
    }

    public void p2EndEvent()
    {
        Debug.Log("EVENT 6");
        byte evCode = 6; // Custom Event 0
        object[] content = new object[] { true }; // Array contains the target position and the IDs of the selected units
        bool reliable = true;
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        PhotonNetwork.RaiseEvent(evCode, content, reliable, raiseEventOptions);
    }

    //EVENTS
    public void OnEvent(byte eventCode, object content, int senderId)
    {
        // Do something
        if (eventCode == 2) //transferência de vida
        {
            if (photonView.isMine)
            {
                object[] data = (object[])content;

                Debug.Log("RECEBEU: " + (float)data[0]);

                float newHealth = health + (float)data[0];
                Debug.Log("newHealth: " + newHealth);
                Debug.Log("viewID: " + photonView.viewID);

                if (newHealth > 1)
                {
                    health = 1;
                    Debug.Log("health + a: " + health);

                }
                else
                {
                    health = newHealth;
                    Debug.Log("health + a: " + health);

                }
            }
            
        }
        if (eventCode == 3) //colisão com hp ball
        {
            if (photonView.isMine)
            {
                Debug.Log("INSIDE EVENT 3");
                object[] data = (object[])content;

                Debug.Log("RECEBEU: " + (float)data[0]);

                float newHealth = health + (float)data[0];
                Debug.Log("newHealth: " + newHealth);
                Debug.Log("viewID: " + photonView.viewID);

                if (newHealth > 1)
                {
                    health = 1;
                    Debug.Log("health + a: " + health);

                }
                else
                {
                    health = newHealth;
                    Debug.Log("health + a: " + health);

                }
            }
        }
        if (eventCode == 4) //colisão com special ball
        {
            if (photonView.isMine)
            {
                object[] data = (object[])content;

                float newSpecial = special + (float)data[0];

                if (newSpecial > 1)
                {
                    special = 1;
                }
                else
                {
                    special = newSpecial;
                }

            }
        }
        if (eventCode == 5) //game over
        {
            object[] data = (object[])content;

            gameIsOver = (bool)data[0];
        }
        if (eventCode == 6) //p2 ondas acabaram
        {
            object[] data = (object[])content;

            P2end = (bool)data[0];
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


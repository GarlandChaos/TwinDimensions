using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject player;
    public float targetDist;
    public float speed;
    public float shootRate;
    public float timeShoot;
    public float timeDamage;
    public float damageWait;
    public float health;
    public float flashDelay;
    public bool takeDamage;
    public Renderer gameMesh;
    public Color normalColor;
    public Color damageColor;
    Material mainMaterial;
    public Material dmgMaterial;
    AudioSource audioSource;
    public AudioClip soundShoot;
    public AudioClip soundDestroyed;
    Rect cameraRect;
    Vector3 bottomLeft;
    Vector3 topRight;
    float timeFollow;
    bool state1;
    bool state2;
    Vector3 waypoint;
    bool adjustPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("playerShip");
        targetDist = 55.0f;
        //speed = 0.5f;
        //shootRate = 0.5f;
        timeShoot = 0.0f;
        timeDamage = 0.0f;
        damageWait = 0.2f;
        //health = 1.0f;
        flashDelay = 0.025f;
        takeDamage = true;
        gameMesh = this.GetComponent<Renderer>();
        normalColor = gameMesh.material.color;
        damageColor = Color.white;
        mainMaterial = gameMesh.material;
        audioSource = this.GetComponent<AudioSource>();

        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 100));

        topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 100));

        cameraRect = new Rect(
         bottomLeft.x,
         bottomLeft.y,
         topRight.x - bottomLeft.x,
         topRight.y - bottomLeft.y);

        timeFollow = 0.0f;

        state1 = true;
        state2 = false;
        adjustPos = false;
    }

    // Update is called once per frame
    void Update()
    {
        huntPlayer();
        controlDamage();
        checkLife();
    }

    void huntPlayer()
    {
        timeShoot += 1.0f * Time.deltaTime;
        timeFollow += 1.0f * Time.deltaTime;

        float d = Vector3.Distance(player.transform.position, transform.position);

        if(timeFollow > 5f)
        {
            timeFollow = 0f;
            if (state1)
            {
                state1 = false;
                chooseNewPath();
            }
            else
            {
                state1 = true;
            }
        }

        if (state1)
        {
            if(adjustPos == false)
            {
                followTarget();
            }
            
        }
        else
        {
            if(adjustPos == true)
            {
                adjustPos = false;
            }
            followNewPath();
        }

        //if (d > targetDist)
        //{

        //    //followTarget();
        //    //chooseNewPath();
        //}
        //else
        //{
        //    //shoot();

        //}
        shoot();
        //print("Distance to other: " + d);

        Vector3 lookPos = player.transform.position - this.transform.position;
        lookPos.x = 0;
        lookPos.y = 0;
        //var rotation = Quaternion.LookRotation(lookPos);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        Quaternion rotation = Quaternion.LookRotation
             (player.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

    }

    void shoot()
    {
        if(timeShoot > shootRate)
        {
            Instantiate(Resources.Load("bullet"), this.transform.position, this.transform.rotation);
            audioSource.PlayOneShot(soundShoot);
            timeShoot = 0.0f;
        }
    }
    
    void followTarget()
    {
        Vector3 dist = new Vector3(player.transform.position.x - this.transform.position.x,
                                   player.transform.position.y - this.transform.position.y,
                                   100);

        transform.position = Vector3.Lerp(transform.position, dist, Time.deltaTime * speed);
    }

    void chooseNewPath()
    {
        waypoint = new Vector3(Random.Range(cameraRect.xMin, cameraRect.xMax),
                                Random.Range(cameraRect.yMin, cameraRect.yMax),
                                   100);

        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, cameraRect.xMin, cameraRect.xMax),
        //                                Mathf.Clamp(transform.position.y, cameraRect.yMin, cameraRect.yMax), 100);
    }

    void followNewPath()
    {

        transform.position = Vector3.Lerp(transform.position, waypoint, Time.deltaTime * speed);

        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, cameraRect.xMin, cameraRect.xMax),
        //                                Mathf.Clamp(transform.position.y, cameraRect.yMin, cameraRect.yMax), 100);
    }

    void controlDamage()
    {
        timeDamage += 1.0f * Time.deltaTime;
        if (timeDamage > damageWait)
        {
            takeDamage = true;
            timeDamage = 0.0f;
        }
    }

    void checkLife()
    {
        if(health <= 0.0f)
        {
            soundController.instance.GetComponent<soundController>().playDestroy();
            WaveManager.instance.GetComponent<WaveManager>().removeEnemy();

            int drop = Random.Range(1, 4);

            if(drop == 1)
            {
                int item = Random.Range(1, 3);
                if(item == 1)
                {
                    Instantiate(Resources.Load("Coletavel_hp"), this.transform.position, player.transform.rotation);

                }
                else
                {
                    Instantiate(Resources.Load("Coletavel_special"), this.transform.position, player.transform.rotation);

                }

            }

            Destroy(this.gameObject);
        }
    }

    private IEnumerator flashColor()
    {
        //gameMesh.material.color = damageColor;
        gameMesh.material = dmgMaterial;
        yield return new WaitForSeconds(flashDelay);
        //gameMesh.material.color = normalColor;
        gameMesh.material = mainMaterial;
        yield return new WaitForSeconds(flashDelay);
    }

    private IEnumerator MoveToPosition(Vector3 newPosition, float time)
    {
        float elapsedTime = 0;

        while (Vector3.Distance(transform.position, newPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            yield return null;

        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bulletPlayer")
        {
            Debug.Log("atingido por tiro");
            if (takeDamage)
            {
                health -= 0.3f;
                //this.GetComponent<Material>().color = Color.white;
                //transform.renderer.material.color = Color.white;
                StartCoroutine(flashColor());
                takeDamage = false;
            }

            Destroy(other.gameObject);

        }
        if(other.tag == "enemy1" || other.tag == "enemy2")
        {
            Debug.Log("inimigos colidiram");
            Vector3 newPos = (this.transform.position - other.transform.position).normalized * 40.0f + other.transform.position;
            //newPos.z = 0;
            transform.position = newPos;
            adjustPos = true;
            //transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
            //StartCoroutine(MoveToPosition(newPos, 1.0f));

        }
        //Debug.Log(other.name);
    }
}

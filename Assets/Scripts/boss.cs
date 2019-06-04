using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    public GameObject player;
    public float targetDist;
    public float speed;
    public float shootRate;
    public float timeShoot;
    public float timeDamage;
    public float timeMove;
    public float moveRate;
    public bool changeDir;
    public bool changeBullet;
    public float damageWait;
    public float health;
    public float flashDelay;
    public bool takeDamage;
    public Renderer gameMesh;
    public Color normalColor;
    public Color damageColor;
    Rect cameraRect;
    Vector3 bottomLeft;
    Vector3 topRight;
    public GameObject cannon1;
    public GameObject cannon2;
    public GameObject cannon3;
    Material mainMaterial;
    public Material dmgMaterial;
    AudioSource audioSource;
    public AudioClip soundShoot;
    public AudioClip soundDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("playerShip");
        targetDist = 55.0f;
        //speed = 0.5f;
        //shootRate = 0.5f;
        timeShoot = 0.0f;
        timeDamage = 0.0f;
        timeMove = 0.0f;
        damageWait = 0.2f;
        //health = 1.0f;
        flashDelay = 0.025f;
        takeDamage = true;
        gameMesh = this.GetComponent<Renderer>();
        normalColor = gameMesh.material.color;
        damageColor = Color.white;
        changeDir = false;
        changeBullet = false;
        mainMaterial = gameMesh.material;

        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 100));

        topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 100));

        cameraRect = new Rect(
         bottomLeft.x,
         bottomLeft.y,
         topRight.x - bottomLeft.x,
         topRight.y - bottomLeft.y);

        audioSource = this.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        move();
        shoot();
        controlDamage();
        checkLife();
    }

    void move()
    {
        timeMove += 1.0f * Time.deltaTime;
        Vector3 move;

        if (timeMove > moveRate)
        {
            if (changeDir)
            {
                changeDir = false;
            }
            else
            {
                changeDir = true;
            }
            timeMove = 0;
        }

        if (changeDir)
        {
            move = Vector3.left;
        }
        else
        {
            move = Vector3.right;
        }

        transform.position += move * speed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, cameraRect.xMin, cameraRect.xMax),
                                        Mathf.Clamp(transform.position.y, cameraRect.yMin, cameraRect.yMax), 100);
    }

    void shoot()
    {
        timeShoot += 1.0f * Time.deltaTime;


        if (timeShoot > shootRate)
        {
            if (changeBullet)
            {
                Vector3 instPos = cannon1.transform.position;
                Quaternion instRot = this.transform.rotation;
                instRot.z = 90;
                Instantiate(Resources.Load("bullet2"), instPos, instRot);
                instPos = cannon2.transform.position;
                Instantiate(Resources.Load("bullet2"), instPos, instRot);
                instPos = cannon3.transform.position;
                Instantiate(Resources.Load("bullet2"), instPos, instRot);
                audioSource.PlayOneShot(soundShoot);
                timeShoot = 0.0f;

                changeBullet = false;
            }
            else
            {
                Vector3 instPos = cannon1.transform.position;
                Quaternion instRot = this.transform.rotation;
                instRot.z = 90;
                Instantiate(Resources.Load("bullet"), instPos, instRot);
                instPos = cannon2.transform.position;
                Instantiate(Resources.Load("bullet"), instPos, instRot);
                instPos = cannon3.transform.position;
                Instantiate(Resources.Load("bullet"), instPos, instRot);
                audioSource.PlayOneShot(soundShoot);
                timeShoot = 0.0f;

                changeBullet = true;
            }
            
        }

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
        if (health <= 0.0f)
        {
            soundController.instance.GetComponent<soundController>().playDestroy();
            WaveManager.instance.GetComponent<WaveManager>().removeEnemy();

            int drop = Random.Range(1, 4);

            if (drop == 1)
            {
                int item = Random.Range(1, 3);
                if (item == 1)
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
    }
}

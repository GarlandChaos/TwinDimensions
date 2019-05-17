using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float moveSpeed = 100f;
    public float flashDelay;

    public Renderer gameMesh;
    public Color normalColor;
    public Color damageColor;
    public playerMove playerComponent;

    Vector3 bottomLeft;

    Vector3 topRight;

    Rect cameraRect;

    Material mainMaterial;
    public Material dmgMaterial;

    // Start is called before the first frame update
    void Start()
    {
        playerComponent = GetComponentInChildren<playerMove>();
        gameMesh = this.GetComponent<Renderer>();
        normalColor = gameMesh.material.color;
        damageColor = Color.white;

        mainMaterial = gameMesh.material;

        flashDelay = 0.025f;

        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 100));

        topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 100));

        cameraRect = new Rect(
         bottomLeft.x,
         bottomLeft.y,
         topRight.x - bottomLeft.x,
         topRight.y - bottomLeft.y);
    }

    // Update is called once per frame
    void Update()
    {
        move();
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(
                                                       mousePos.x,
                                                       mousePos.z,
                                                       mousePos.y));
        ////Vector3 forward = mouseWorld - this.transform.position;
        ////this.transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
        //transform.LookAt(mouseWorld);
        Vector3 screen_pos = Camera.main.WorldToScreenPoint(this.transform.position);

        float AngleRad = Mathf.Atan2(mousePos.y - screen_pos.y, mousePos.x - screen_pos.x) * Mathf.Rad2Deg;
        // Get Angle in Degrees
        //float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleRad - 90f);
        if (playerComponent == null)
        {
            playerComponent = GetComponentInChildren<playerMove>();
        }
    }

    private void move()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * moveSpeed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, cameraRect.xMin, cameraRect.xMax),
                                        Mathf.Clamp(transform.position.y, cameraRect.yMin, cameraRect.yMax), 100);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.other.tag == "collectibleHp")
    //    {
    //        Debug.Log("colidiu com caixa de hp");
    //    }
    //}


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
        if (other.tag == "collectibleHp")
        {
            Debug.Log("colidiu com caixa de hp");
            if (playerComponent != null)
            {
                Debug.Log("PLAYER NOT NULL, SHOULD EXECUTE");
                //playerComponent.collisionHP();
                playerComponent.collectibleHPEvent();
            }
            Destroy(other.gameObject);

        }
        if (other.tag == "collectibleSpecial")
        {
            Debug.Log("colidiu com caixa de especial");
            if (playerComponent != null)
            {
                //playerComponent.collisionSpecial();
                playerComponent.collectibleSpecialEvent();
            }
            Destroy(other.gameObject);

        }
        if (other.tag == "bullet" || other.tag == "bullet2")
        {
            //Debug.Log("atingido por tiro");
            //if (takeDamage)
            //{
                //health -= 0.1f;
                //healthImage.fillAmount = health;
                StartCoroutine(flashColor());
            if (playerComponent != null)
            {
                playerComponent.receiveDamage();
            }
            //GetComponentInChildren<playerMove>().receiveDamage();
            //}

            Destroy(other.gameObject);

        }
        //Debug.Log(other.name);
    }


}

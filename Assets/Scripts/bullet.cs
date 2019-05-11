using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Vector3 direction;
    public float time;
    public float lifeSpan;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("playerShip");

        if (this.tag == "bullet"){
            direction = new Vector3(player.transform.position.x - this.transform.position.x,
                                player.transform.position.y - this.transform.position.y,
                                0);

            Vector3 lookPos = player.transform.position - this.transform.position;
            lookPos.x = 0;
            lookPos.y = 0;

            Quaternion rotation = Quaternion.LookRotation
                 (player.transform.position - transform.position, transform.TransformDirection(Vector3.up));
            transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

            this.transform.Rotate(new Vector3(0, 0, 90));


            speed = 1.0f;
        }
        else if(this.tag == "bullet2")
        {
            direction = new Vector3(0, -1, 0);
            speed = 40.0f;
        }


        time = 0.0f;
        lifeSpan = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        seekDirection();
        checkLife();
    }

    void checkLife()
    {
        time += 1.0f * Time.deltaTime;
        if(time > lifeSpan)
        {
            time = 0.0f;
            Destroy(this.gameObject);
        }
    }
    void seekDirection()
    {
        transform.position += direction * Time.deltaTime * speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    Vector3 direction;
    public float time;
    public float lifeSpan;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("playerShip");
        //direction = new Vector3(0, 1, 0);
        direction =  player.transform.position - this.transform.position;
        direction = -Vector3.Normalize(direction);

        //this.transform.Rotate(new Vector3(0, 0, 90));
        time = 0.0f;
        lifeSpan = 5.0f;
        speed = 40.0f;
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
        if (time > lifeSpan)
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

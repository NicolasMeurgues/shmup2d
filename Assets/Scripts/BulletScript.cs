using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    Rigidbody2D rb;

    public int ySpeedMultiplicator;
    int dir = 1;
    float xSpeed = 0.0f;


    public bool isLaser;

    private void Awake()
    {
        if(!isLaser)
            rb = GetComponent<Rigidbody2D>();
    }

    //Check and potentially change the direction of the bullets at every frame
    void Update()
    {
        if(!isLaser)
            rb.velocity = new Vector2(3*xSpeed, ySpeedMultiplicator*dir);
    }


    //Force the bullets to go from up to bottom. Useful when the enemies are shooting.
    public void ChangeDirectionY()
    {
        dir *= -1;
    }

    //Changes the velocity of the bullet on the X axis. Useful for different spray patterns.
    public void ChangeDirectionX(float i)
    {
        xSpeed += i;
    }

    //On collision, check what it collided with.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dir == 1 && !isLaser)
        {
            if (collision.gameObject.tag == "Ennemy")
            {
                collision.gameObject.GetComponent<Ennemy>().Damage();
                Destroy(gameObject);
            }
            if (collision.gameObject.tag == "bounds")
            {
                Destroy(gameObject);
            }
            if (collision.gameObject.tag == "Boss1")
            {
                collision.gameObject.GetComponent<Boss1>().Damage();
                Destroy(gameObject);
            }
        }
        else if (dir == -1 && !isLaser)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerScript>().Damage();
                Destroy(gameObject);
            }
            if (collision.gameObject.tag == "bounds")
            {
                Destroy(gameObject);
            }
        }

        else if (isLaser)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerScript>().Damage();
                Debug.Log("attention au laser");
            }
        }
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    Rigidbody2D rb;

    int dir = 1;
    int xSpeed = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(3*xSpeed, 15*dir);
    }

    public void ChangeDirectionY()
    {
        dir *= -1;
    }

    public void ChangeDirectionX(int i)
    {
        xSpeed += i;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dir == 1)
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
        }
        else
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
        
    }

}

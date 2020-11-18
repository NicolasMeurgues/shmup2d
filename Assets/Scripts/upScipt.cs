using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpScipt : MonoBehaviour
{
    Rigidbody2D rb;

    int dir = -1;
    int xSpeed = 0;


    public int boost;


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
        rb.velocity = new Vector2(0 * xSpeed, 3 * dir);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (boost)
            {
                case 1:
                    collision.gameObject.GetComponent<PlayerScript>().AddShield();
                    break;
                case 2:
                    collision.gameObject.GetComponent<PlayerScript>().UpgradeShoot();
                    break;
                case 3:
                    collision.gameObject.GetComponent<PlayerScript>().AddLife();
                    break;
            }
                
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "bounds")
        {
            Destroy(gameObject);
        }

    }
}

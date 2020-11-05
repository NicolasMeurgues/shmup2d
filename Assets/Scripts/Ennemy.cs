﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Ennemy : MonoBehaviour
{

    GameObject a;
    public GameObject bullet, explosion;
    Rigidbody2D rb;


    public bool moveX;
    float xSpeed;
    public float ySpeed;
    float j;

    public bool shooter;
    public float attackSpeed;
    public float pv;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (shooter)
        {
            InvokeRepeating("Shoot", attackSpeed, attackSpeed);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (moveX) 
        {
            xSpeed = math.cos(j)*5;
            j += 0.02f;
        }
        rb.velocity = new Vector2(xSpeed, ySpeed*-1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerScript>().Damage();
            Die();
        }
        if (collision.gameObject.tag == "destructor")
        {
            Destroy(gameObject);
        }
    }

    void Die()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Damage()
    {
        pv--;
        if (pv == 0)
        {
            Die();
        }
    }

    void Shoot()
    {
        GameObject temp = (GameObject) Instantiate(bullet, transform.position, Quaternion.identity);
        temp.GetComponent<BulletScript>().ChangeDirection();
    }

}

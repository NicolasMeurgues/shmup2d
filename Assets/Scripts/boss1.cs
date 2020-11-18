using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System;
using UnityEngine.SceneManagement;

public class Boss1 : MonoBehaviour
{

    float xSpeed;
    Rigidbody2D rb;
    public GameObject bullet, a1, a2, b1, b2, laser1, laser2;
    public bool alive = true;
    public int health = 900;
    public float attackSpeedSeconds;
    public float repeatAttackSpeed;
    float j = 0;
    float rotation = 0.0f;

    public int bossPhase = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        a1 = transform.Find("a1").gameObject;
        a2 = transform.Find("a2").gameObject;
        b1 = transform.Find("b1").gameObject;
        b2 = transform.Find("b2").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("BossFunction", attackSpeedSeconds, repeatAttackSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        xSpeed = math.cos(j) * 2.0f;
        j += 0.01f;


        //Check the boss phase based on its HP
        if (health <= 900 && health > 600)
            bossPhase = 1;
        else if (health <= 600 && health > 300) 
        { 
            bossPhase = 2;

        }
        else if (health <= 300 && health > 0)
            bossPhase = 3;
        else if (health <= 0)
            Die();
        rb.velocity = new Vector2(xSpeed, 0);
    }


    void BossFunction()
    {
        if (bossPhase == 1)
            BossShootPhase1();
        else if (bossPhase == 2)
            BossShootPhase2();
        else if (bossPhase == 3)
            BossShootPhase1();
    }


    void BossShootPhase1()
    {
        float shootSpeedMultiplier = UnityEngine.Random.Range(1.0f, 2.0f);

        GameObject temp = (GameObject)Instantiate(bullet, a1.transform.position, Quaternion.identity);
        GameObject temp2 = (GameObject)Instantiate(bullet, a2.transform.position, Quaternion.identity);
        temp.GetComponent<BulletScript>().ChangeDirectionY();
        temp.GetComponent<BulletScript>().ChangeDirectionX(math.cos(rotation) * 3);
        temp2.GetComponent<BulletScript>().ChangeDirectionY();
        temp2.GetComponent<BulletScript>().ChangeDirectionX(math.cos(rotation) * -3);
        rotation += 0.2f;
    }

    void BossShootPhase2()
    {
            laser1.SetActive(true);
            laser2.SetActive(true);
    }

    public void Damage()
    {
        health--;
    }


    void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Credits");
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerScript>().Damage();
            Debug.Log("attention au laser");
        }
    }

}


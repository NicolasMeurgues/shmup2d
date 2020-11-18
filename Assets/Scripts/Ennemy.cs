using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public GameObject bullet, explosion, upShoot, upShield, heart;
    Rigidbody2D rb;

    public int score;

    public bool moveX;
    float xSpeed;
    public float ySpeed;
    float j;

    public int shooter;
    public float attackSpeed;
    public int pv;

    public float dropRate = 0.2f;


    //Get the rigidbody of the enemy at spawn
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //Check which type of shooting pattern the spawned enemy has
    void Start()
    {
        if (shooter == 1)
        {
            float randAtk = UnityEngine.Random.Range(0f, 1f);
            InvokeRepeating("Shoot", attackSpeed + randAtk, attackSpeed);
        }

        if (shooter == 2)
        {
            float randAtk = UnityEngine.Random.Range(0f, 1f);
            InvokeRepeating("Shoot2", attackSpeed + randAtk, attackSpeed);
        }

        if (shooter == 3)
        {
            float randAtk = UnityEngine.Random.Range(0f, 1f);
            InvokeRepeating("Shoot3", attackSpeed + randAtk, attackSpeed);
        }
    }

    //If the enemy is tagged as being able to move on the X axis, then change its horizontal velocity 
    //to make it match a sine move pattern
    void Update()
    {
        if (moveX)
        {
            xSpeed = math.cos(j) * 5;
            j += 0.02f;
        }
        rb.velocity = new Vector2(xSpeed, ySpeed * -1);
    }


    //When colliding with an object, check whether it is the player or the destructor.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If it is the player, inflict damage then destruct self
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerScript>().Damage();
            Die();
        }
        //Else, destruct self.
        if (collision.gameObject.tag == "destructor")
        {
            Destroy(gameObject);
        }
    }

    //When the spawned enemy is killed, check whether it drops a power-up or not
    //This is pseudo-random and should give a fair amount of power-ups, which are essential
    void Die()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + score);
        Destroy(gameObject);
        float drop = UnityEngine.Random.Range(0f, 1f);
        if (drop < dropRate)
        {
            float up = UnityEngine.Random.Range(0, 3);
            switch (up)
            {
                case 0: //shields upgrade
                    Instantiate(upShield, transform.position, Quaternion.identity);
                    break;
                case 1: //shooting speed upgrade
                    Instantiate(upShoot, transform.position, Quaternion.identity);
                    break;
                case 2: //extra life
                    Instantiate(heart, transform.position, Quaternion.identity);
                    break;
            }
            
        }

    }

    //Simple damage function.
    public void Damage()
    {
        pv--;
        if (pv == 0)
        {
            Die();
        }
    }

    //When shooting, instantiate and spawn a bullet relative to the rigidbody 
    void Shoot()
    {
        GameObject temp = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
        temp.GetComponent<BulletScript>().ChangeDirectionY();
    }

    //Alternate shooting pattern. Shoots two bullets diagonally.
    void Shoot2()
    {
        GameObject temp1 = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
        GameObject temp2 = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
        temp1.GetComponent<BulletScript>().ChangeDirectionY();
        temp1.GetComponent<BulletScript>().ChangeDirectionX(1);
        temp2.GetComponent<BulletScript>().ChangeDirectionY();
        temp2.GetComponent<BulletScript>().ChangeDirectionX(-1);
    }

    //Alternate shooting pattern. Shoots five bullets at once in a spray pattern.
    void Shoot3()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject temp = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            temp.GetComponent<BulletScript>().ChangeDirectionY();
            temp.GetComponent<BulletScript>().ChangeDirectionX(-2 + i);
        }

    }
}

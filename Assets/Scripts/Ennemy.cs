using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public GameObject bullet, explosion;
    Rigidbody2D rb;

    public int score;

    public bool moveX;
    float xSpeed;
    public float ySpeed;
    float j;

    public int shooter;
    public float attackSpeed;
    public int pv;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if (moveX)
        {
            xSpeed = math.cos(j) * 5;
            j += 0.02f;
        }
        rb.velocity = new Vector2(xSpeed, ySpeed * -1);
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
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + score);
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
        GameObject temp = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
        temp.GetComponent<BulletScript>().ChangeDirectionY();
    }

    void Shoot2()
    {
        GameObject temp1 = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
        GameObject temp2 = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
        temp1.GetComponent<BulletScript>().ChangeDirectionY();
        temp1.GetComponent<BulletScript>().ChangeDirectionX(1);
        temp2.GetComponent<BulletScript>().ChangeDirectionY();
        temp2.GetComponent<BulletScript>().ChangeDirectionX(-1);
    }

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

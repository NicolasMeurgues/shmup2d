using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class PlayerScript : MonoBehaviour
{
    GameObject a, b;
    public GameObject bullet, explosion;

    public Vector2 moveSpeed = new Vector2(17, 17);
    private Vector2 movement;

    public int life = 3;
    public int attackSpeed = 9;
    int attackRate = 0;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        a = transform.Find("a").gameObject;
        b = transform.Find("b").gameObject;
    }

    private void Start()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt(0.ToString()));
        PlayerPrefs.SetInt("Life", PlayerPrefs.GetInt(0.ToString()) + 3);
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        movement = new Vector2(
            moveSpeed.x * inputX,
            moveSpeed.y * inputY);


    }
    void FixedUpdate()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = movement;
        if (Input.GetKey(KeyCode.Space) && attackRate > attackSpeed)
        {
            Shoot();
        }
        attackRate++;
    }

    public void Damage()
    {
        life--;
        PlayerPrefs.SetInt("Life", PlayerPrefs.GetInt("Life") - 1);
        if (life == 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameOverMenu.instance.OnPlayerDeath();
        }
    }

    public void AddShield()
    {

    }

    public void AddLife()
    {
        if (life < 5)
        {
            life++;
            PlayerPrefs.SetInt("Life", PlayerPrefs.GetInt("Life") + 1);
        }
        
    }

    public void UpgradeShoot()
    {
        if (attackSpeed > 5) { attackSpeed -= 2; }
    }

    void Shoot()
    {
        attackRate = 0;
        Instantiate(bullet, a.transform.position, Quaternion.identity);
        Instantiate(bullet, b.transform.position, Quaternion.identity);
    }

}







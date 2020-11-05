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

    public float moveSpeed;
    public int life = 3;
    int attackSpeed=0;

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
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * moveSpeed, 0));
        rb.AddForce(new Vector2(0, Input.GetAxis("Vertical") * moveSpeed));

        if (Input.GetKey(KeyCode.Space) && attackSpeed > 50)
        {
            Shoot();
        }
        attackSpeed++;
    }

    public void Damage()
    {
        life--;
        PlayerPrefs.SetInt("Life", PlayerPrefs.GetInt("Life") - 1);
        if (life == 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        attackSpeed = 0;
        Instantiate(bullet, a.transform.position, Quaternion.identity);
        Instantiate(bullet, b.transform.position, Quaternion.identity);
    }

}

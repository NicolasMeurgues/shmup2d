using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    //Those two gameObjects gives the blasters' positions.
    GameObject a, b;

    public GameObject bullet, explosion, shield;

    //Player parameters
    public Vector2 moveSpeed = new Vector2(17, 17);
    private Vector2 movement;
    public int life = 3, attackSpeed = 9;
    int shieldedTime = 0, maxShieldTime = 120, attackRate = 0;
    bool isShield = false;

    //Sound objects
    public AudioSource audioSource;
    public AudioClip laserSound, shieldOn, shieldOff, loseSound;

    public Rigidbody2D rb;

    //Gets the ship rigidBody, and the two blasters' positions.
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        a = transform.Find("a").gameObject;
        b = transform.Find("b").gameObject;
    }

    //Resets the score and the life counter
    private void Start()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt(0.ToString()));
        PlayerPrefs.SetInt("Life", PlayerPrefs.GetInt(0.ToString()) + 3);
    }

    //Checks keyboard inputs and move the ship accordingly
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        movement = new Vector2(
            moveSpeed.x * inputX,
            moveSpeed.y * inputY);


    }

    //Calls the shooting function independently from the framerate
    void FixedUpdate()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = movement;
        if (Input.GetKey(KeyCode.Space) && attackRate > attackSpeed)
        {
            Shoot();
        }
        attackRate++;
        
        //Shield powerup : checks the shield time left, and apply or remove shield accordingly
        if (isShield && shieldedTime >= maxShieldTime)
        {
            audioSource.PlayOneShot(shieldOff);
            shield.SetActive(false);
            isShield = false;
            shieldedTime = 0;
        }
        else if (isShield) { shieldedTime++; }
    }

    //If the player is damaged : 
    //remove a life on both the player parameters and the UI, then play the hit sound. 
    //If the player has no more lives, destroy and displays the game over UI.
    public void Damage()
    {
        if (!isShield)
        {
            life--;
            PlayerPrefs.SetInt("Life", PlayerPrefs.GetInt("Life") - 1);
            AudioManager.instance.PlayClipAt(loseSound, transform.position);
            if (life == 0)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
                GameOverMenu.instance.OnPlayerDeath();
            }
        }
    }

    //Shield powerup. Activate shields and play the according sound.
    public void AddShield()
    {
        if (!isShield) 
        {
            isShield = true;
            shield.SetActive(true);
            audioSource.PlayOneShot(shieldOn); 
        }
    }

    //Extra life. Adds an extra life until a maximum of 10 lives. 
    public void AddLife()
    {
        if (life < 10)
        {
            life++;
            PlayerPrefs.SetInt("Life", PlayerPrefs.GetInt("Life") + 1);
        }
    }

    //Shooting speed upgrade. Pretty self-explanatory.
    public void UpgradeShoot()
    {
        if (attackSpeed > 5) { attackSpeed -= 2; }
    }

    //Instanciate two bullets on the blasters' position.
    void Shoot()
    {
        audioSource.PlayOneShot(laserSound);
        attackRate = 0;
        Instantiate(bullet, a.transform.position, Quaternion.identity);
        Instantiate(bullet, b.transform.position, Quaternion.identity);
    }

}







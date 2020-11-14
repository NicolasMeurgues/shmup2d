using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public float rate;
    public GameObject[] enemies;
    public int waves = 1;
    public static bool isBoss = false;


    public GameObject boss1;



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", rate, rate);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Score") >= 2000 && !isBoss)
        {
            spawnBoss1();
        }
    }

    void SpawnEnemy()
    {
        if (!isBoss)
        {
            for (int i = 0; i < waves; i++)
            {
                Instantiate(enemies[(int)Random.Range(0, enemies.Length)], new Vector3(Random.Range(-6.5f, 6.5f), 7, 0), Quaternion.identity);

            }
        }
    }


    void spawnBoss1()
    {
        isBoss = true;
        boss1.SetActive(true);
    }

}

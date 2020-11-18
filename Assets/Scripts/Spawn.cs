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


    //Resets boss state and spawns enemies
    void Start()
    {
        InvokeRepeating("SpawnEnemy", rate, rate);
        isBoss = false;
        
    }

    //Updates the score
    void Update()
    {
        if (PlayerPrefs.GetInt("Score") >= 30000 && !isBoss)
        {
            spawnBoss1();
        }
    }

    //Spawns enemies unless the boss state is triggered
    void SpawnEnemy()
    {
        if (!isBoss)
        {
            for (int i = 0; i < waves; i++)
            {
                Instantiate(enemies[(int)Random.Range(0, enemies.Length)], new Vector3(Random.Range(-3.0f, 3.0f), 7, 0), Quaternion.identity);

            }
        }
    }

    //Self-explanatory
    void spawnBoss1()
    {
        isBoss = true;
        boss1.SetActive(true);
    }

}

using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime, intialTime;
    public int spawnTotalNumber = 4;
    public static int numberToKill = 5;
    public static int numberKilled;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", intialTime, spawnTime);
        numberKilled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(numberKilled >= numberToKill)
        {
            //you win
        }
    }

    void SpawnEnemy()
    {
        if(spawnTotalNumber > 0)
        {
            Vector3 enemyPosition = transform.position;

            GameObject spawnedEnemy = Instantiate(enemyPrefab, enemyPosition, transform.rotation) as GameObject;

            spawnedEnemy.transform.parent = gameObject.transform;

            spawnTotalNumber--;
        }

    }
}

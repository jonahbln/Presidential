using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime, intialTime;
    public int spawnTotalNumber = 4;
    void Start()
    {
        InvokeRepeating("SpawnEnemy", intialTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {

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

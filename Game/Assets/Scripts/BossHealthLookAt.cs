using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthLookAt : MonoBehaviour
{
   

    GameObject player;
    public float moveSpeed = 100f;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }



    void Update()
    {

        Vector3 target = new Vector3(player.transform.position.x,
            transform.position.y, player.transform.position.z);
        transform.LookAt(target);
        if(GetComponentInChildren<BossAI>().rolling)
        {
            transform.position = transform.forward * moveSpeed * Time.deltaTime;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y +120f, transform.rotation.z);
    }
  
}

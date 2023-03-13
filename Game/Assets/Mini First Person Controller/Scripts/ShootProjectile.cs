using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public int fireInterval = 3;
    public GameObject projectilePrefab;
    public float forceAmount = 40f;

    void Start()
    {
        InvokeRepeating("fire", fireInterval, fireInterval);
    }

    
    void Update()
    {

    }

    void fire()
    {
        GameObject proj = Instantiate(projectilePrefab, transform.position, transform.localRotation);

        proj.GetComponent<Rigidbody>().AddForce(transform.forward * forceAmount, ForceMode.Impulse);
    }
}

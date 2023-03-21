using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketHitBack : MonoBehaviour
{
    public Camera cam;
    public GameObject ballPrefab;
    public AudioClip hitSFX;
    void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 direction = hit.point - transform.position;
                direction.y = 0f;
                direction.Normalize();

                GameObject ball = Instantiate(ballPrefab, transform.position + direction, Quaternion.identity);
                Rigidbody ballRb = ball.GetComponent<Rigidbody>();
                ballRb.AddForce(direction * 500f);
                AudioSource.PlayClipAtPoint(hitSFX, transform.position);
            }
        }
    }
}

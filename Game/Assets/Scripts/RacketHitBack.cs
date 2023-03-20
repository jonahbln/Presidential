using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketHitBack : MonoBehaviour
{
    public Camera cam;
    public GameObject ballPrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera through the reticle
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Draw a debug line to visualize the ray
                Debug.DrawLine(ray.origin, hit.point, Color.red);

                // Calculate the swing direction
                Vector3 direction = hit.point - transform.position;
                direction.y = 0f;
                direction.Normalize();

                // Apply a force to the ball in the swing direction
                GameObject ball = Instantiate(ballPrefab, transform.position + direction, Quaternion.identity);
                Rigidbody ballRb = ball.GetComponent<Rigidbody>();
                ballRb.AddForce(direction * 500f);
            }
        }
    }
}

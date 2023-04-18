using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketBehavior : MonoBehaviour
{
    public float delayTime = 0.5f;
    Animator anim;
    bool canSwing = true;
    public AudioClip frontSwing;
    public AudioClip backSwing;
    public Camera cam;
    public GameObject ballPrefab;
    public AudioClip hitSFX;

    public bool moving;

    void Start()
    {
        moving = false;
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.gamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && canSwing)
            {
                anim.SetInteger("SwingInt", 1);
                canSwing = false;
                AudioSource.PlayClipAtPoint(frontSwing, transform.position);
                Shoot();
                Invoke("Delay", delayTime);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && canSwing)
            {
                anim.SetInteger("SwingInt", 2);
                canSwing = false;
                AudioSource.PlayClipAtPoint(backSwing, transform.position);
                Shoot();
                Invoke("Delay", delayTime);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                anim.SetInteger("SwingInt", 3);
                canSwing = false;
                PlayerController.speed = 5f;
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                anim.SetInteger("SwingInt", 0);
                canSwing = true;
                PlayerController.speed = 10f;
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                PlayerController.speed = 15f;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                PlayerController.speed = 10f;
            }

            anim.SetBool("Moving", moving);
        }
    }

    void Delay()
    {
        anim.SetInteger("SwingInt", 0);
        canSwing = true;
    }

    void Shoot()
    {
        if (!LevelManager.gamePaused)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~LayerMask.GetMask("Enemy")))
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

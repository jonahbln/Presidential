using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static float speed = 10f;
    public float jumpHeight = 1f;
    public float gravity = 9.8f;
    public float airControl = 10f;

    CharacterController controller;
    Vector3 input;
    Vector3 moveDirection;
    bool grounded = true;
    bool walking = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        transform.GetChild(1).GetChild(0).gameObject.GetComponent<AudioSource>().volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal!= 0 || moveVertical != 0)
        {
            walking = true;
        }
        else
        {
            walking = false;
        }
        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;

        GetComponentInChildren<RacketBehavior>().moving = walking;


        if (controller.isGrounded)
        {


            if (!grounded)
            {
                grounded = true;
                // land SFX
                transform.GetChild(1).GetChild(1).gameObject.GetComponent<AudioSource>().Play();
            }

            moveDirection = input;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
                // jump SFX
                transform.GetChild(1).GetChild(2).gameObject.GetComponent<AudioSource>().Play();
            }
            else
            {
                moveDirection.y = 0.0f;
            }

            if (walking)
            {
                // walk SFX
                transform.GetChild(1).GetChild(0).gameObject.GetComponent<AudioSource>().volume = 1;
            }
            else
            {
                transform.GetChild(1).GetChild(0).gameObject.GetComponent<AudioSource>().volume = 0;
            }
        }
        else
        {

            grounded = false;
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }

        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * speed * Time.deltaTime);
    }
}

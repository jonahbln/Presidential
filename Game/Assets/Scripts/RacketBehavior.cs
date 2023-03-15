using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketBehavior : MonoBehaviour
{
    public float delayTime = 0.5f;
    Animator anim;
    bool canSwing = true;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && canSwing)
        {
            anim.SetInteger("SwingInt", 1);
            canSwing = false;
            Invoke("Delay", delayTime);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && canSwing)
        {
            anim.SetInteger("SwingInt", 2);
            canSwing = false;
            Invoke("Delay", delayTime);
        }

    }

    void Delay()
    {
        anim.SetInteger("SwingInt", 0);
        canSwing = true;
    }
}

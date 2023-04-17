using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresidentBehavior : MonoBehaviour
{

    public static bool transformation = false;
    bool calledAnim = false;
    Animator anim;
    bool appQuit;

    public GameObject boss;
    public GameObject explosion;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        transformation = false;
        calledAnim = false;
        appQuit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(transformation && !calledAnim)
        {
            anim.SetTrigger("Transformation");
            calledAnim = true;
            Destroy(gameObject, 4.5f);
        }
    }

    private void OnApplicationQuit()
    {
        appQuit = true;
    }

    private void OnDestroy()
    {
        if (appQuit) return;
        Instantiate(boss, transform.position, transform.rotation);
        GameObject g = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(g, 2f);
    }

}

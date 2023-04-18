using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public enum FSMStates
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead
    }

    public FSMStates currentState;
    public GameObject player;
    public GameObject explosion;
    public GameObject enemy;
    public Transform enemyStartPoint;

    GameObject levelManager;

    Animator anim;
    Vector3 nextDestination;
    float distanceToPlayer;
    float elapsedTime = 0f;
    float origY;

    public float moveSpeed = 5f;

    public bool rolling = false;
    bool appQuit = false;

    NavMeshAgent agent;

    void Start()
    {
        appQuit = false;
        rolling = false;
        Initialize();

    }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //transform.LookAt(player.transform.position);

        switch (currentState)
        {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Dead:
                UpdateDeadState();
                break;
        }

        elapsedTime += Time.deltaTime;
    }

    private void Initialize()
    {
        currentState = FSMStates.Patrol;
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        origY = transform.position.y;
    }

    private void UpdateDeadState()
    {
        rolling = false;
        anim.SetInteger("animState", 4);
        Destroy(gameObject, 2f);
    }

    private void UpdateAttackState()
    {

        anim.SetInteger("animState", 3);
        rolling = false;

        if (elapsedTime > 2f)
        {
            Instantiate(enemy, enemyStartPoint.position, enemyStartPoint.rotation);
            elapsedTime = 0f;
            currentState = FSMStates.Patrol;
        }


    }

    private void OnApplicationQuit()
    {
        appQuit = true;
    }

    private void UpdateChaseState()
    {

        rolling = true;

        if (elapsedTime > 8f)
        {
            elapsedTime = 0f;
            currentState = FSMStates.Attack;
        }

        FaceTarget(player.transform.position);

        anim.SetInteger("animState", 2);

        //transform.position = transform.forward * moveSpeed * Time.deltaTime;

    }

    private void UpdatePatrolState()
    {
        rolling = false;

        anim.SetInteger("animState", 1);

        if (elapsedTime > 3f)
        {
            elapsedTime = 0f;
            currentState = FSMStates.Chase;
        }

    }

    private void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(lookRotation.x, lookRotation.y + 90f, lookRotation.z), Time.deltaTime * 5);
    }

    private void OnDestroy()
    {
        if (appQuit) return;
        GameObject g = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(g, 2f);
        levelManager.GetComponent<LevelManager>().Win();
        
    }

    public void Dead()
    {
        currentState = FSMStates.Dead;
    }

}
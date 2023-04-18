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

    NavMeshAgent agent;

    void Start()
    {
        Initialize();

    }

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        origY = transform.position.y;
    }

    private void UpdateDeadState()
    {
        anim.SetInteger("animState", 4);
        Destroy(gameObject, 2f);
    }

    private void UpdateAttackState()
    {

        anim.SetInteger("animState", 3);


        if (elapsedTime > 2f)
        {
            Instantiate(enemy, enemyStartPoint.position, enemyStartPoint.rotation);
            elapsedTime = 0f;
            currentState = FSMStates.Patrol;
        }


    }

    private void UpdateChaseState()
    {

        if (elapsedTime > 8f)
        {
            elapsedTime = 0f;
            currentState = FSMStates.Attack;
        }

        anim.SetInteger("animState", 2);

        FaceTarget(player.transform.position);

    }

    private void UpdatePatrolState()
    {

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
        if (PresidentBehavior.appQuit) return; 
        GameObject g = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(g, 2f);
        levelManager.GetComponent<LevelManager>().Win();
        
    }

    public void Dead()
    {
        currentState = FSMStates.Dead;
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
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
    public float enemySpeed = 5f;
    public float chaseDistance = 10f;
    public float attackDistance = 5f;
    public GameObject player;
    public GameObject spellProjectile;
    public GameObject wandTip;
    public float shootRate = 2f;
    public float projSpeed = 20f;

    GameObject[] wanderPoints;
    Animator anim;
    Vector3 nextDestination;
    int currentDestinationIndex = 0;
    float distanceToPlayer;
    float elapsedTime = 0f;
    float origY;

    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        switch(currentState)
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
        wanderPoints = GameObject.FindGameObjectsWithTag("wanderPoint");
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        //wandTip = GameObject.FindGameObjectWithTag("wandTip");
        origY = transform.position.y;
        FindNextPoint();
    }

    private void UpdateDeadState()
    {

    }

    private void UpdateAttackState()
    {
        print("Attacking!");

        nextDestination = player.transform.position;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);

        anim.SetInteger("animState", 3);

        EnemySpellCast();
    }

    private void EnemySpellCast()
    {
        if(elapsedTime >= shootRate)
        {
            var animDuration = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("SpellCasting", animDuration);
            elapsedTime = 0f;
        }
    }

    void SpellCasting()
    {
        GameObject proj = Instantiate(spellProjectile, wandTip.transform.position, wandTip.transform.rotation);
        proj.GetComponent<Rigidbody>().AddForce(proj.transform.forward * projSpeed, ForceMode.Impulse);
    }

    private void UpdateChaseState()
    {
        print("Chasing!");

        nextDestination = player.transform.position;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }

        FaceTarget(nextDestination);

        transform.position = Vector3.MoveTowards(transform.position, nextDestination, Time.deltaTime * enemySpeed);
        transform.position = new Vector3(transform.position.x, origY, transform.position.z);
    }

    private void UpdatePatrolState()
    {
        print("Patrolling!");

        anim.SetInteger("animState", 1);

        if(Vector3.Distance(transform.position, nextDestination) < 1)
        {
            FindNextPoint();
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }

        FaceTarget(nextDestination);

        transform.position = Vector3.MoveTowards(transform.position, nextDestination, Time.deltaTime * enemySpeed);
        transform.position = new Vector3(transform.position.x, origY, transform.position.z);
    }

    private void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    private void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;

        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}

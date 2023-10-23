using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace GameInterfaces {
public class Wolf : EnemyController
{

//TODO: wolf get hit animation

    Vector3 initPos;
    Vector3 wayPoint;
    [SerializeField] float patrolRange;
    

    void Awake()
    {
        // this.detectRange = 15f;
        // this.attackRange = 5f;
        EnemyState = EnemyState.IDLE;

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.stoppingDistance = attackRange;
        

        animator = GetComponent<Animator>();
        healthManager = GetComponent<HealthManager>();

        initPos = transform.position;
        NewWayPoint();

    }

    void Start()
    {

        
    }


    void Update()
    {
        attackTimer += Time.deltaTime;
        knockBackTimer += Time.deltaTime;
        SwitchStates();
        SwitchAnimation();
        resetVel();
    }

    private void SwitchStates()
    {
        if (detectTarget() && EnemyState != EnemyState.DEAD)
        {
            EnemyState = EnemyState.CHASE;
            agent.speed = moveSpeed;
            
        }

        switch (EnemyState)
        {
            case EnemyState.IDLE:
                
                agent.speed = moveSpeed*0.3f;

                if(Vector3.Distance(transform.position,wayPoint) <= agent.stoppingDistance){
                    
                    NewWayPoint();
                }else{
                    
                    agent.destination = wayPoint;

                }
                break;

            case EnemyState.CHASE:
                agent.speed = moveSpeed;
                if (!attackTarget)
                {
                    //out of detect range, bo back to idle
                    Debug.Log(transform.name + "ltarget out of range");
                    isRunning = false;
                    agent.destination = transform.position;
                    EnemyState = EnemyState.IDLE;
                    

                }
                else
                {
                    //in detect range, chase target
                    isRunning = true;
                    agent.destination = attackTarget.transform.position;

                    //attack if in attack range
                    if (Vector3.Distance(transform.position, attackTarget.transform.position) <= attackRange && (attackTimer > attackInterval))
                    {
                        //Debug.Log(transform.name + "attacking");
                        animator.SetTrigger("isAttack");
                        //Attack();
                    }

                }


                break;
            case EnemyState.DEAD:
                isRunning = false;
                isDead = true;
                agent.destination = transform.position;

                break;
        }

    }

    private bool detectTarget()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRange);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Carriage" || collider.tag == "Player")
            {
                //Debug.Log(transform.name + "found carriage");
                attackTarget = collider.gameObject;

                return true;
            }
        }
        attackTarget = null;
        return false;

    }

    private void NewWayPoint(){
        float randomX = Random.Range(-patrolRange, patrolRange);
        float randomZ = Random.Range(-patrolRange, patrolRange);
        Vector3 randomPoint = new Vector3(initPos.x + randomX, initPos.y, initPos.z + randomZ);

        NavMeshHit hit;
        wayPoint = UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit,patrolRange, 1) ? hit.position : transform.position;
        agent.destination = wayPoint;

    }



    // public override void Die()
    // {
    //     //Debug.Log(transform.name + "Dies");
    //     isDead = true;
    //     EnemyState = EnemyState.DEAD;
    //     //Destroy(gameObject, 2f);
    // }

}

}
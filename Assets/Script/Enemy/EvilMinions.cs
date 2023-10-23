using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameInterfaces {
public class EvilMinions : EnemyController
{
    /*Evil knights: spawn randomly at runtime and attacks the carriage.
        switch atttack target to player if player is in detect range

        spawn at random position
    */

    //private Animator animator;
    void Awake()
    {

        EnemyState = EnemyState.CHASE; //always chase

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.stoppingDistance = attackRange-2f;
        
        animator = GetComponent<Animator>();
        healthManager = GetComponent<HealthManager>();
        attackTarget = GameObject.FindGameObjectWithTag("Carriage");

    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        knockBackTimer += Time.deltaTime;
        agent.destination = attackTarget.transform.position;
        SwitchStates();
        SwitchAnimation();
        resetVel();

        
    }

    private void SwitchStates()
    {
        switch (EnemyState)
        {
            case EnemyState.CHASE:
                // if found player, follows player instead of carriage
                // switch target back to carriage if player is out of range
                if (!detectPlayer())
                {
                    attackTarget = GameObject.FindGameObjectWithTag("Carriage");
                }

                
                    //attack if in attack range
                    if ((Vector3.Distance(transform.position, attackTarget.transform.position) < attackRange)&& (attackTimer > attackInterval))
                    {
                        //isAttacking = true;
                        //Debug.Log(transform.name + "attacking");
                        animator.SetTrigger("isAttack");
                        //Attack();
                        
                        //Debug.Log(transform.name + attackTimer);
                    }
                    else
                    {
                        //Debug.Log(transform.name +Vector3.Distance(transform.position, attackTarget.transform.position));
                        //isAttacking = false;
                    }
                break;
            case EnemyState.DEAD:
                isRunning = false;
                isDead = true;
                attackTarget = this.gameObject; // set attack target to itself to stop moving
                agent.isStopped = true;
                break;

        }
    }

    private bool detectPlayer()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRange);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Player")
            {
                //Debug.Log(transform.name + "found Player");
                attackTarget = collider.gameObject;
                return true;
            }
        }
        return false;

    }

}

}
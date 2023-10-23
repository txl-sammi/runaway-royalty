using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInterfaces;


namespace GameInterfaces {
public enum EnemyState
{
    IDLE,
    CHASE,
    DEAD
}

public class EnemyController : MonoBehaviour, Damageable
{
    [SerializeField] protected float detectRange;
    [SerializeField] protected float attackRange;
    [SerializeField] protected int attack;
    [SerializeField] protected float attackInterval;
    [SerializeField] protected float moveSpeed;
    protected float knockbackTime = 1f;


    protected EnemyState EnemyState;
    protected bool isRunning = false;
    protected bool isWalk = true;
    protected bool isDead = false;


    protected GameObject attackTarget;
    protected UnityEngine.AI.NavMeshAgent agent;

    protected Animator animator;
    protected HealthManager healthManager;

    protected float attackTimer = 0f;
    protected float knockBackTimer = 0f;

//TODO:health Bar

    // void Update()
    // {
    //     attackTimer += Time.deltaTime;
    // }

    public void ReceiveDamage(int damageAmount,Vector3 attackerPosition)
    {
        //apply knockback effect
        //obstacle will not be knockbacked
        Vector3 knockBackDirection = (transform.position - attackerPosition).normalized;
        knockBack(knockBackDirection);

        healthManager.ApplyDamage(damageAmount);
        animator.SetTrigger("isHit");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public virtual void Die() //obstacle override this method
    {
        //Debug.Log(transform.name + "Dies");
        EnemyState = EnemyState.DEAD;
        Destroy(gameObject, 10);
    }



    protected void SwitchAnimation()
    {
        animator.SetBool("isRunning", isRunning);
        //animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isDead", isDead);
        
    }

    protected void Attack()
    {
        attackTimer = 0f;
        
        //TODO: refactor this

            if (attackTarget.CompareTag("Player")){
            attackTarget.GetComponent<PlayerScript>().ReceiveDamage(this.attack,this.transform.position);
            }
            else if (attackTarget.CompareTag("Carriage")){
            attackTarget.GetComponent<Cart>().ReceiveDamage(this.attack,this.transform.position);
            }

    }

    // TODO: finish this method
    protected GameObject selectCarriageAttackPoint()
    {
       // GameObject carriage = GameObject.FindGameObjectsWithTag("Carriage");
        //GameObject attackPoint = GameObject.FindGameObjectsWithTag("Carriage").gameObject.ransform.GetChild(0).gameObject;

        GameObject c = GameObject.FindGameObjectsWithTag("Carriage")[0];
        GameObject attackPoint = c.transform.GetChild(0).gameObject;

        int randomIndex = Random.Range(0, attackPoint.transform.childCount);
        //Debug.Log("ATTACK POINT " + randomIndex);
        return attackPoint.transform.GetChild(randomIndex).gameObject;
    }


    protected void knockBack(Vector3 dir){

        

        // Rigidbody rb = this.GetComponent<Rigidbody>();
        // if (rb != null)
        // {
        //     //rb.AddForce(dir * 2f, ForceMode.Impulse);
        //     rb.velocity = dir * 5f;
        // }
        if(agent){
                    agent.isStopped = true;
            agent.velocity = dir * 7f;
        }
        



        
        //this.transform.position += dir * 2f;
    }

    protected void resetVel(){
        // if(knockBackTimer > knockbackTime){
        //     Rigidbody rb = this.GetComponent<Rigidbody>();
        //     if (rb != null)
        //     {
        //         rb.velocity = Vector3.zero;
        //     }
        //     knockBackTimer = 0f;
        // }

        agent.isStopped = false;
        agent.speed = this.moveSpeed;
    }
    

}
}
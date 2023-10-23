using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInterfaces;

namespace GameInterfaces {
public class PlayerScript : MonoBehaviour,Damageable
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private GameInput gameInput;
    private CharacterAudio playerAudio;

    [SerializeField] private float interactRange;
    [SerializeField] private float attackRange;
    [SerializeField] private int attack; //maybe move to charater state later?
    private Vector3 moveDir;
    private Vector3 stairsNormalVector;
    private float raycastDistance;
    private bool isWalking = false;
    private bool isAttacking = false;
    [SerializeField] private float minX, maxX, minZ, maxZ;

    private Inventory inventory;
    private HealthManager healthManager;

    [SerializeField] private float invenciableTime = 0.5f;
    private float receiveDamagetimer = 0f;
    [SerializeField] private InventoryHolder holder;
    [SerializeField] private InventoryItemData itemToLookFor;
    [SerializeField] private int healValue;

    void Awake()
    {
        playerAudio = transform.Find ("Audio").GetComponent<CharacterAudio>();
        inventory = new Inventory();
        healthManager = GetComponent<HealthManager>();
    }

    void Start()
    {
        raycastDistance = transform.position.y * 0.5f;
    }

    void Update()
    {
        receiveDamagetimer += Time.deltaTime;

        //get input
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        float padding = 2.5f;
        float x = transform.position.x - Camera.main.transform.position.x;
        float z = transform.position.z - Camera.main.transform.position.z;
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, z)).x - padding;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, z)).x + padding;
        minZ = Camera.main.ViewportToWorldPoint(new Vector3(x, 0, 0)).z;

        //keep input seperate from actual movement!
        moveDir = new Vector3(inputVector.x, 0f, inputVector.y);//change to 3d

        Vector3 newPos = transform.position + (moveDir * moveSpeed * Time.deltaTime);

        if (newPos.x > minX && newPos.x < maxX && newPos.z > minZ) {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
        if (newPos.x <= minX) {
            newPos.x = minX;
            transform.position = newPos;
        }
        if (newPos.x >= maxX) {
            newPos.x = maxX;
            transform.position = newPos;
        }
        if (newPos.z <= minZ) {
            newPos.z = minZ;
            transform.position = newPos;
        }
        

        // smooth transition when roatating
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        if (moveDir.magnitude >= 0.1f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }


        if (gameInput.GetInteractKeyDown())
        {
            //playerInteract();
        }

        if (gameInput.GetAttackKeyDown())
        {
            
            isAttacking = true;
            //Attack(); move to animation event
        }
        else
        {
            isAttacking = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (holder.InventorySystem.ContainsItem(itemToLookFor, out List<InventorySlot> existSlot))
            {
                if (holder.InventorySystem.RemoveFromInventory(itemToLookFor, 1))
                {
                    healthManager.Heal(healValue);
                }
            }
        }

    }

    public void ReceiveDamage(int damageAmount,Vector3 attackerPosition)
    {
        //Debug.Log("Player receive damage");
        if(receiveDamagetimer > invenciableTime){
            healthManager.ApplyDamage(damageAmount);
            this.GetComponent<Animator>().SetTrigger("IsHit");
            receiveDamagetimer = 0f;
        }
        
    }


    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }


    //refactor needed to seperate input
    /*
    private void playerInteract()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliders)
        {
            //Debug.Log(hitCollider.name);
            if (collider.CompareTag("Interactable"))
            {
                collider.GetComponent<Interactable>().Interact();
            }
        }
    }
    */

    void Attack()
    {
        playerAudio.PlayAttack();
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider collider in colliders)
        {
            Vector3 directionToCollider = collider.transform.position - transform.position;
            if (collider.CompareTag("Enemy") || collider.CompareTag("Obstacle") || collider.CompareTag("Crystal"))
            {
                // Calculate angle between player's forward direction and direction to enemy
                float angle = Vector3.Angle(transform.forward, directionToCollider);
                if (angle <= 90)
                {
                    // Collider is in front of player
                    Debug.Log("Attacking enemy: " + collider.name);
                    if (collider.GetComponent<EnemyController>())
                    {
                        collider.GetComponent<EnemyController>().ReceiveDamage(this.attack,this.transform.position);
                    }
                    
                }

            }
        }
    }



    //For debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }

    public void Die(){
        GameManager.instance.GameOver("Player is dead");
    }

}

}
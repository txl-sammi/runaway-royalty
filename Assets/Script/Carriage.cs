using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum CarriageState
{
    NORMAL,
    HUNGRY
}
public class Carriage : MonoBehaviour
{

    //[SerializeField]protected float moveSpeed;
    [SerializeField]protected float raycastDistance;
    [SerializeField]protected GameObject destination;
    private UnityEngine.AI.NavMeshAgent carriageAgent;
    private GameObject horse;
    private Animator horseAnimator;

    private Vector3 dest;
    private CarriageState CarriageState;

    void Awake(){
        
        dest = new Vector3(destination.transform.position.x, 0, destination.transform.position.z);

        this.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(dest);
        
    }

    void Start()
    {
        carriageAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        horse = GameObject.FindGameObjectWithTag("HorseAnimator");
        CarriageState = CarriageState.NORMAL;
        if (horse != null)
        {
            horseAnimator = horse.GetComponent<Animator>();
        }
    }
    
    void Update()
    {
        SwitchState();
    }

    void SwitchState(){
        switch(CarriageState){
            case CarriageState.NORMAL:
                move();
                if(reachedDestination())
                {
                    SceneManager.LoadScene("PassGame");
                }
                break;

            case CarriageState.HUNGRY:
                carriageAgent.isStopped = true;
                if(reachedDestination())
                {
                    SceneManager.LoadScene("PassGame");
                }
                break;
        }
    }

    void move(){

        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            // If an obstacle is detected, stop moving
            carriageAgent.isStopped = true;
            horseAnimator.enabled = false;
            //Debug.Log("Obstacle detected! Carriage stopped.");
        }
        
        else{
            //this.transform.position += transform.forward * moveSpeed * Time.deltaTime;
            //Debug.Log("Obstacle removed");
            carriageAgent.isStopped = false;
            rotateWheel();
            horseAnimator.enabled = true;
        }
        
    }

    bool reachedDestination(){
        if(Vector3.Distance(transform.position, dest) < 5f){
            return true;
        }
        else{
            return false;
        }
    }

    private void rotateWheel(){
        Vector3 rotationVector = new Vector3(100, 0, 0)* Time.deltaTime;
        transform.Find("Cart/visual/front_wheel").Rotate(rotationVector);
        transform.Find("Cart/visual/rear_wheel").Rotate(rotationVector);
    }

    public void setCarriageStateNormal(){
        this.CarriageState = CarriageState.NORMAL;
    }

    public void setCarriageStateHungry(){
        this.CarriageState = CarriageState.HUNGRY;
        Debug.Log("horse is hungry");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, InteractableInterface
{
    [SerializeField] private string prompt;
    [SerializeField] public GameObject objectToSpawn;
    [SerializeField] public int maxWood;
    public string InteractionPrompt => prompt;
    protected Animator animator;

    private int wood = 0;
    //[SerializeField] private Animator treeShakeAnim;

    public void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        
    }

    public bool Interact(Interactor interactor)
    {
        animator.SetTrigger("isHit");
        //Debug.Log("Player is interacting with a tree!");

        if (wood < maxWood)
        {
            GameObject newObject = Instantiate(objectToSpawn, transform.position, transform.rotation);
            wood += 1;
            float distanceInFront = -3.0f;
            Vector3 newPosition = transform.position + transform.forward * distanceInFront;
            newObject.transform.position = newPosition;
        }
        
        
        return true;
    }
}

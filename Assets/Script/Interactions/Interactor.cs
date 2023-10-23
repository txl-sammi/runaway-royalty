using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private InteractionPromptUI interactionPromptUI;


    private Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound; 

    private InteractableInterface interactable;
    private GameObject interactableObject;

    private Highlight lastHighlight;


    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);

        if (numFound > 0) 
        {
            interactable = colliders[0].GetComponent<InteractableInterface>();
            interactableObject = colliders[0].gameObject;
            if (interactable != null)
            {
                //Debug.Log(interactable);
                if (!interactionPromptUI.isDisplayed){
                    interactionPromptUI.setUp(interactable.InteractionPrompt);

                    if(interactableObject.TryGetComponent<Highlight>(out Highlight highlight))
                    {
                        highlight.HighlightObject();
                        lastHighlight = highlight;
                    }
                } 

                if (Input.GetKeyDown(KeyCode.F)) interactable.Interact(this);
            }

        }
        else
        {
            if (interactable != null) {
                interactable = null;
                if(lastHighlight != null){
                    lastHighlight.UnHighlight();
                }
            }
            if (interactionPromptUI.isDisplayed){
                interactionPromptUI.Close();
                if(lastHighlight != null){
                    lastHighlight.UnHighlight();
                }
            } 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }

}

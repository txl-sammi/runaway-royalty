using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalFrag : MonoBehaviour, InteractableInterface
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        //Debug.Log("Player is interacting with crystal fragment!");

        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Horse : MonoBehaviour, InteractableInterface
{
    [SerializeField] private string prompt;
    [SerializeField] private InventoryHolder inventory;
    [SerializeField] private InventoryItemData itemToLookFor;
    [SerializeField] private HealthManager health;
    [SerializeField] private int healValue;

    public string InteractionPrompt => prompt;

    [SerializeField] private GameObject missingItemText;

    public bool Interact(Interactor interactor)
    {
        //Debug.Log("Player is interacting with horse!");

        // check if player is holding a carrot
        // then increase horse energy
        if (inventory.InventorySystem.RemoveFromInventory(itemToLookFor, 1))
        {
            health.Heal(healValue);
            return true;
        }
        else {
            // missing wood
            showMissingItem("Missing food to feed the horse");
        }
        
        
        return false;
    }

    void showMissingItem(string text)
    {
        
        if(missingItemText)
        {   
            Debug.Log("showing text");
            GameObject prefab = Instantiate(missingItemText, missingItemText.transform.position, Quaternion.identity) as GameObject;
            prefab.GetComponentInChildren<TextMeshProUGUI>().text = text;
            prefab.transform.SetParent(GameObject.FindGameObjectWithTag("UICanvas").transform, false);
        }
    }
}

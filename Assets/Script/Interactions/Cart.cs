using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInterfaces;
using TMPro;

namespace GameInterfaces {
public class Cart : MonoBehaviour, InteractableInterface,Damageable
{

    //-------------------------INTERACTIONS------------------------
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    [SerializeField] private InventoryHolder inventory;
    [SerializeField] private InventoryItemData woodToLookFor;
    [SerializeField] private InventoryItemData flowerToLookFor;
    
    private HealthManager healthManager;
    [SerializeField] private int healValue;
    [SerializeField] private HealthManager princessHealth;

    [SerializeField] private GameObject missingItemText;

    public bool Interact(Interactor interactor)
    {
        //Debug.Log("Player is interacting with cart! Time to prompt if they would like to help the cart or the princess!");
        // check if player is holding a flower or woody
        // increase princess happiness or carriage health 

        if (inventory.InventorySystem.RemoveFromInventory(woodToLookFor, 1))
        {
            healthManager.Heal(healValue);
            return true;
            
        }
        else {
            // missing wood
            showMissingItem("Missing wood to fix the carriage");
        }

        if (inventory.InventorySystem.RemoveFromInventory(flowerToLookFor, 1))
        {
                princessHealth.Heal(healValue);
                return true;
        }
        else {
            // missing flower
            showMissingItem("Missing item needed to please the princess");
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


    //-------------------------HEALTH------------------------------
    void Awake()
    {
        healthManager = GetComponent<HealthManager>();
    }

    public void ReceiveDamage(int damageAmount,Vector3 attackerPosition)
    {
        healthManager.ApplyDamage(damageAmount);
    }

    public void Die(){
        Debug.Log("Carriage is broken");
        GameManager.instance.GameOver("Carriage is broken");
    }
}

}
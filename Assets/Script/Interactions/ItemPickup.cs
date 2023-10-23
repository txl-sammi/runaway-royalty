using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class ItemPickup : MonoBehaviour
{
    public float PickUpRadius = 1f;
    public InventoryItemData ItemData;
    private SphereCollider sCollider;
    private bool canPickup = false;
    private GameObject collidedObject;

    void Awake()
    {
        sCollider = GetComponent<SphereCollider>();
        sCollider.isTrigger = true;
        sCollider.radius = PickUpRadius;
    }

    private void OnTriggerStay(Collider other)
    {
        collidedObject = other.gameObject;

        var inventory = other.transform.GetComponent<InventoryHolder>();

        if (inventory)
        {
            canPickup = true;

            if (Input.GetKeyDown(KeyCode.F))
            {
                TryPickupItem(inventory);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canPickup = false;

        if (collidedObject == other.gameObject)
        {
            collidedObject = null;
        }
    }

    void Update()
    {
        // Pick up the item even if the player is already inside the trigger zone
        if (canPickup && collidedObject != null && Input.GetKeyDown(KeyCode.F))
        {
            var inventory = collidedObject.transform.GetComponentInParent<InventoryHolder>();
            TryPickupItem(inventory);
        }
    }

    private void TryPickupItem(InventoryHolder inventory)
    {
        if (inventory.InventorySystem.AddToInventory(ItemData, 1))
        {
            Destroy(this.gameObject);
        }
    }
}
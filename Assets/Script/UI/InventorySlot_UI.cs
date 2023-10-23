using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot_UI : MonoBehaviour
{
    public Sprite slotIcon;
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private InventorySlot assignedInventorySlot;
     
    public Text itemInfoText;
    public InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public InventoryDisplay ParentDisplay { get; private set; }

    private void Awake()
    {
        ClearSlot();

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
       
    }

    public void Init(InventorySlot slot)
    {
        assignedInventorySlot = slot;
        UpdateUISlot(slot);
    }

    public void UpdateUISlot(InventorySlot slot)
    {
        if(slot != null && slot.ItemData != null)
        {
            // Generate the slot icon
            itemSprite.sprite = slot.ItemData.Icon;
            itemSprite.color = Color.white;
            
            // Display the number of the item stored
            if (slot.StackSize > 1) 
            {
                itemCount.text = slot.StackSize.ToString();
            }
            else 
            {
                itemCount.text = "";
            }
        }
        else
        {
            itemSprite.sprite = slotIcon;
            itemCount.text = "";
            if (itemInfoText != null)
            {
                itemInfoText.text = "";
            }
        }

        if (itemInfoText != null)
        {
            itemInfoText.text = slot.ItemData.DisplayName;
        }
    }

    public void ClearSlot()
    {
        assignedInventorySlot?.ClearSlot();
        //itemSprite.sprite = null;
        //itemSprite.color = Color.clear;
        itemCount.text = "";

        if (itemInfoText != null)
        {
            itemInfoText.text = "";
        }
    }



    public string GetItemName()
    {
        if (itemInfoText != null)
        {
            return itemInfoText.text;
        }
        else
        {
            return "Unknown"; 
        }

    }
}

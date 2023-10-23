using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverUIInventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infoImage;
    private Text infoText; // Reference to the Text component in the infoImage
    private bool isHovering = false;
    private Coroutine hideInfoCoroutine;
    public InventorySlot_UI assignedInventorySlot;

    [SerializeField] float offsetX = 0;
    [SerializeField] float offsetY = 0;


    void Start()
    {
        // Find the Text component in the infoImage
        infoText = infoImage.GetComponentInChildren<Text>();
    }

    void Update()
    {
        if (infoImage.activeSelf)
        {
            // Update the infoImage position
            infoImage.transform.position = Input.mousePosition + new Vector3(offsetX, offsetY, 0);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (infoImage != null)
        {
            // Set the text in the infoImage
            string itemName = assignedInventorySlot.GetItemName();
            infoText.text = itemName;

            infoImage.SetActive(true);
            isHovering = true;
            if (hideInfoCoroutine != null)
            {
                StopCoroutine(hideInfoCoroutine);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse exited UI element");
        if (infoImage != null)
        {
            
            isHovering = false;
            hideInfoCoroutine = StartCoroutine(HideInfoPanelWithDelay());
        }
    }

    private IEnumerator HideInfoPanelWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        if (!isHovering)
        {
            infoImage.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        isHovering = false;
        if (hideInfoCoroutine != null)
        {
            StopCoroutine(hideInfoCoroutine);
        }
    }

}

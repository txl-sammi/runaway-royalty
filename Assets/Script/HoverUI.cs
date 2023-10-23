using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject infoImage;
    public string Name;
    private bool isHovering = false;
    private Coroutine hideInfoCoroutine;

    [SerializeField] float offsetX = 0;
    [SerializeField] float offsety = 0;

  
    void Update()
    {
        if (infoImage.activeSelf)
        {
            infoImage.transform.position = Input.mousePosition + new Vector3(offsetX, offsety, 0);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (infoImage != null)
        {
            infoImage.SetActive(true);
            infoImage.GetComponentInChildren<Text>().text = Name;
            isHovering = true;
            if (hideInfoCoroutine != null)
            {
                StopCoroutine(hideInfoCoroutine);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissingItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private GameObject uiPanel;

    private void Start()
    {
        uiPanel.SetActive(false);
    }

    public bool isDisplayed = false;

    public void setUp(string prompt)
    {
        promptText.text = prompt;
        uiPanel.SetActive(true);
        isDisplayed = true;
    }

    public IEnumerator Close()
    {
        Debug.Log("closing");
        yield return new WaitForSeconds (2);
        uiPanel.SetActive(false);
        isDisplayed = false;
    }

}

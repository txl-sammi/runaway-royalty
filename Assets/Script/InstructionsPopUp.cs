using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsPopUp : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject canvas;

    private bool isPressed = false;

    private void Start()
    {
        Time.timeScale = 0;
        instructionsPanel.SetActive(true);
        canvas.SetActive(false);
    }

    private void Update()
    {
        if (isPressed)
        {
            Time.timeScale = 1;
        }
    }

    public void BeginGame()
    {
        isPressed = true;
        Time.timeScale = 1;
        instructionsPanel.SetActive(false);
        canvas.SetActive(true);
    }
}

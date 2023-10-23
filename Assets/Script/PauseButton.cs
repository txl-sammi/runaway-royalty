using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public Button pauseButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject canvas;

    private bool isGamePaused = false;

    private void Start()
    {
        pauseButton.onClick.AddListener(TogglePauseGame);
    }

    private void TogglePauseGame()
    {
        if (isGamePaused)
        {
            Time.timeScale = 1f;
            isGamePaused = false;
        }
        else
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            canvas.SetActive(false);
        }
    }
}

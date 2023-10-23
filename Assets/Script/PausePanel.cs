using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject canvas;

    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        canvas.SetActive(true);
    }
}

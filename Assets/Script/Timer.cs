using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text countdownText;  
    public float totalTime = 300f;  
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;

       
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        
        if (currentTime <= 0)
        {
            
            GameOver();
        }
    }

    private void GameOver()
    {

        SceneManager.LoadScene("StartScene");
    }
}

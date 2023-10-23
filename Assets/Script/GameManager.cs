using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject GameoverMenu;
    void Awake(){
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(string message){
        //TODO: Pass message to GameoverMenu
        Debug.Log(message);
        GameoverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

}

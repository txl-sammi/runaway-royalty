using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Princess : MonoBehaviour
{
    public void Depressed(){
        GameManager.instance.GameOver("Princess is depressed");
    }
}

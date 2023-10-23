using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    [SerializeField] private float secondsToDestroy = 3f;
    
    void Start()
    {
        Destroy(gameObject, secondsToDestroy);    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraFunctionP : MonoBehaviour
{
    public Image healthPhoto;
    public HealthManager healthManager;

    private void Start()
    {
        
        healthManager.onHealthChanged.AddListener(UpdateHealthPhotoVisibility);
    }

    private void UpdateHealthPhotoVisibility()
    {
        
        if (healthManager.CurrentHealth < 50)
        {
            healthPhoto.enabled = true;
        }
        else
        {
            healthPhoto.enabled = false;
        }
    }
}

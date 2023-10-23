using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameInterfaces {
public class Obstacle : EnemyController
{

    void Awake()
    {
        healthManager = GetComponent<HealthManager>();
        animator = GetComponent<Animator>();
        
        if (animator != null)
        {
            // TODO: Add obstacle animation (particle effect & become smaller when hit)
        }
        else
        {
            Debug.LogWarning("Animator component not found on " + gameObject.name);
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
    }

}
}

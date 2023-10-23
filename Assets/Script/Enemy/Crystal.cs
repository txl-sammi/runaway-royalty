using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameInterfaces {
public class Crystal : EnemyController
{
    [SerializeField] public GameObject objectToSpawn;
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
        SpawnCrystalFragments();
        Destroy(gameObject);
    }

    private void SpawnCrystalFragments()
    {
        Vector3 newPosition = transform.position + transform.right * -1f;
        Quaternion newRoatation = transform.rotation * Quaternion.Euler (90f, 0f, 90f);

        GameObject.Instantiate(objectToSpawn, newPosition, newRoatation);
    }

}
}
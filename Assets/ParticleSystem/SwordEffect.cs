using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEffect : MonoBehaviour
{
    private ParticleSystem particle_system;

    void Start(){
                // Get a reference to the Particle System component
        particle_system = GetComponentInChildren<ParticleSystem>();

        // Disable the Particle System (optional)
        particle_system.Stop();
    }

    public void PlayEffect()
    {
        // var particles = Instantiate(effect, transform.position, Quaternion.identity);
        // p = particles;
        // particles.transform.SetParent(transform);
        // particles.transform.position = transform.position;

        particle_system.Play();
        Debug.Log("Effect played");
    }



}

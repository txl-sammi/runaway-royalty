using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [SerializeField] private AudioSource attack;
    [SerializeField] private AudioSource hurt;
    [SerializeField] private AudioSource death;

    public void PlayAttack() {
        attack.Play();
    }

    public void PlayHurt() {
        hurt.Play();
        Debug.Log(transform.parent.name + " Play Hurt Audio");
    }

    public void PlayDeath() {
        death.Play();
    }

    public void debugmessage(){
        Debug.Log("called");
    }
}

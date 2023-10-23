using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] public int startingHealth;
    [SerializeField] private float healthChange;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] public UnityEvent onHealthChanged;
    [SerializeField] public UnityEvent onDamageTaken;
    
    public float _currentHealth;
    public float CurrentHealth{
        get{
            return _currentHealth;
        }
        set{
            _currentHealth = value;
            onHealthChanged.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    public void ResetHealth(){
        CurrentHealth = this.startingHealth;
    }

    public void Heal(int healAmount){
        if(CurrentHealth + healAmount > startingHealth){
            CurrentHealth = startingHealth;
        }else{
            CurrentHealth += healAmount;
            //onHealthChanged.Invoke();
        }
    }

    public void ApplyDamage(int damage){
        CurrentHealth -= damage;
        //Debug.Log(transform.name+ " health=" +CurrentHealth);
        onDamageTaken.Invoke();
    }

    
    void Update ()
    {
        CurrentHealth -= healthChange * Time.deltaTime;
        //onHealthChanged.Invoke();
         if(_currentHealth <= 0){
            onDeath.Invoke();
        }
    }
}

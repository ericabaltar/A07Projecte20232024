using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField]int health;
    [SerializeField]int maxHealth;
    public UnityEvent<int, int> OnHealthAltered;
    public UnityEvent OnDie;
    private void Start()
    {
        if(maxHealth <= 0)
            maxHealth = 1;
        health = maxHealth;
    }
    public void Heal()
    {
        this.health++;
        OnHealthAltered.Invoke(health,maxHealth);
        if (this.health > this.maxHealth)
            this.health = this.maxHealth;
    }
    public void Heal(int healingVal)
    {
        this.health+= healingVal;
        OnHealthAltered.Invoke(health, maxHealth);
        if (this.health > this.maxHealth)
            this.health = this.maxHealth;
    }
    public void FullHeal() {
        OnHealthAltered.Invoke(health, maxHealth);
        this.health = this.maxHealth;
    }
    public void Damage()
    {
        this.health--;
        OnHealthAltered.Invoke(health, maxHealth);
        if (this.health <= 0)
            OnDie.Invoke();
    }
    public void Damage(int damageVal)
    {
        this.health -= damageVal;
        OnHealthAltered.Invoke(health, maxHealth);
        if (this.health <= 0)
            OnDie.Invoke();
    }
    public void Kill()
    {
        this.health = 0;
        OnHealthAltered.Invoke(health, maxHealth);
        OnDie.Invoke();
    }
}

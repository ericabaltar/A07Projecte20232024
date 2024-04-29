using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth;
    public UnityEvent<int, int> OnHealthAltered;
    public UnityEvent OnDie;

    public Image healthBar; // Referencia a la imagen de la barra de vida
    public Text damageText; // Referencia al texto que muestra el daño recibido

    private void Start()
    {
        if (maxHealth <= 0)
            maxHealth = 1;
        health = maxHealth;

        UpdateHealthBarColor();
    }

    public void Heal()
    {
        this.health++;
        OnHealthAltered.Invoke(health, maxHealth);
        if (this.health > this.maxHealth)
            this.health = this.maxHealth;

        UpdateHealthBarColor();
    }

    public void Heal(int healingVal)
    {
        this.health += healingVal;
        OnHealthAltered.Invoke(health, maxHealth);
        if (this.health > this.maxHealth)
            this.health = this.maxHealth;

        UpdateHealthBarColor();
    }

    public void FullHeal()
    {
        OnHealthAltered.Invoke(health, maxHealth);
        this.health = this.maxHealth;

        UpdateHealthBarColor();
    }

    public void Damage()
    {
        this.health--;
        OnHealthAltered.Invoke(health, maxHealth);
        if (this.health <= 0)
            OnDie.Invoke();

        UpdateHealthBarColor();
        ShowDamageText();
    }

    public void Damage(int damageVal)
    {
        this.health -= damageVal;
        OnHealthAltered.Invoke(health, maxHealth);
        if (this.health <= 0)
            OnDie.Invoke();

        UpdateHealthBarColor();
        ShowDamageText();
    }

    public void Kill()
    {
        this.health = 0;
        OnHealthAltered.Invoke(health, maxHealth);
        OnDie.Invoke();

        UpdateHealthBarColor();
    }

    private void UpdateHealthBarColor()
    {
        if (healthBar != null)
        {
            float healthRatio = (float)health / (float)maxHealth;
            healthBar.fillAmount = healthRatio;

            // Cambiar el color de la barra de vida según la cantidad de vida restante
            if (healthRatio > 0.5f)
                healthBar.color = Color.green;
            else if (healthRatio > 0.2f)
                healthBar.color = Color.yellow;
            else
                healthBar.color = Color.red;
        }
    }

    private void ShowDamageText()
    {
        if (damageText != null)
        {
            damageText.text = "Damage: " + (maxHealth - health).ToString();

            // Cambiar el color del texto de daño a rojo
            damageText.color = Color.red;
        }
    }
}
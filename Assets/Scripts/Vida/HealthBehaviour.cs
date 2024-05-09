using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth;
    public UnityEvent<int, int> OnHealthAltered;
    public UnityEvent OnDie;

    public Image healthBar;

    private int totalDamageTaken = 0;
    private Coroutine damageCoroutine;

    private void Start()
    {
        if (maxHealth <= 0)
            maxHealth = 1;
        health = maxHealth;

        UpdateHealthBarColor();
    }

    public void Damage()
    {
        Damage(1);
    }

    public void Damage(int damageVal)
    {
        this.health -= damageVal;
        totalDamageTaken += damageVal;
        OnHealthAltered.Invoke(health, maxHealth);
        if (this.health <= 0)
            Die();

        UpdateHealthBarColor();

        // Reiniciar el contador de daño si ya se está ejecutando
        if (damageCoroutine != null)
            StopCoroutine(damageCoroutine);
        damageCoroutine = StartCoroutine(ResetDamageAfterDelay());
    }

    private IEnumerator ResetDamageAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        // Limpiar el texto después de 2 segundos
        totalDamageTaken = 0; // Reiniciar el total de daño acumulado
    }

    private void UpdateHealthBarColor()
    {
        if (healthBar != null)
        {
            float healthRatio = (float)health / (float)maxHealth;
            healthBar.fillAmount = healthRatio;

            if (healthRatio > 0.5f)
                healthBar.color = Color.white;
            else if (healthRatio > 0.2f)
                healthBar.color = Color.white;
            else
                healthBar.color = Color.white;
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(3);
    }

    // Método para restaurar la salud al máximo al recoger una poción
    public void RestoreMaxHealth()
    {
        health = maxHealth;
        OnHealthAltered.Invoke(health, maxHealth);
        UpdateHealthBarColor();
    }
}

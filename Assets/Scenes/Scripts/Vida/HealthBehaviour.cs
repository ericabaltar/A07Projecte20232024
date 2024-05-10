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
    public SpriteRenderer characterSpriteRenderer; // Referencia al SpriteRenderer del personaje

    private int totalDamageTaken = 0;
    private Coroutine damageCoroutine;

    private AudioSource PJDamageSound;

    private void Start()
    {
        if (maxHealth <= 0)
            maxHealth = 1;
        health = maxHealth;

        UpdateHealthBarColor();

        PJDamageSound = GetComponent<AudioSource>();
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

        if (PJDamageSound != null)
            PJDamageSound.Play();

        // Cambia el color del personaje a rojo cuando recibe daño
        StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed()
    {
        // Cambia el color del personaje a rojo
        characterSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f); // Mantén el color rojo durante 0.1 segundos
        // Restaura el color original del personaje
        characterSpriteRenderer.color = Color.white;
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

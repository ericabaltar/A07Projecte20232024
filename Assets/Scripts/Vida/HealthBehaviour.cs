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
    public TextMeshProUGUI damageText;

    private int totalDamageTaken = 0;
    private Coroutine damageCoroutine;

    private void Start()
    {
        if (maxHealth <= 0)
            maxHealth = 1;
        health = maxHealth;

        UpdateHealthBarColor();

        damageText.text = "";
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
        // Mostrar el daño en grande y rojo
        damageText.fontSize = 40;
        damageText.color = Color.red;

        float startY = damageText.rectTransform.localPosition.y;
        float targetY = startY + 50;

        yield return new WaitForSeconds(2f);

        // Gradualmente reducir el tamaño y la opacidad, y subir el texto
        float duration = 2f;
        float timer = 0f;
        float startSize = 40f;
        float endSize = 20f;
        Color startColor = Color.red;
        Color endColor = new Color(1f, 0f, 0f, 0f);

        while (timer < duration)
        {
            float t = timer / duration;
            damageText.fontSize = Mathf.Lerp(startSize, endSize, t);
            damageText.color = Color.Lerp(startColor, endColor, t);
            float newY = Mathf.Lerp(startY, targetY, t);
            damageText.rectTransform.localPosition = new Vector3(damageText.rectTransform.localPosition.x, newY, damageText.rectTransform.localPosition.z);
            yield return null;
            timer += Time.deltaTime;
        }

        // Limpiar el texto después de 2 segundos
        totalDamageTaken = 0; // Reiniciar el total de daño acumulado
        damageText.text = ""; // Limpiar el texto
    }

    private void UpdateHealthBarColor()
    {
        if (healthBar != null)
        {
            float healthRatio = (float)health / (float)maxHealth;
            healthBar.fillAmount = healthRatio;

            if (healthRatio > 0.5f)
                healthBar.color = Color.green;
            else if (healthRatio > 0.2f)
                healthBar.color = Color.yellow;
            else
                healthBar.color = Color.red;
        }
    }

    private void Update()
    {
        if (totalDamageTaken > 0)
        {
            damageText.text = totalDamageTaken.ToString();
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

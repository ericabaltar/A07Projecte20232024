using System.Collections;
using UnityEngine;

public class OrcSystem : MonoBehaviour
{
    [SerializeField] private float meleeAttackDamage = 10f;
    [SerializeField] private float meleeAttackRange = 1.5f;
    private HealthBehaviour playerHealth;
    [SerializeField] private BarraDeVidaOrco barraDeVidaOrco;
    [SerializeField] private float maxHealth;
    private bool canAttack = true; // Variable para controlar el cooldown
    private float health;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isRunning = false;
    private bool isAttacking = false;
    private bool inMeleeRange = false;

    private void Start()
    {
        health = maxHealth;
        barraDeVidaOrco.UpdateHealthBar(maxHealth, health);
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBehaviour>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Verifica si el jugador está dentro del rango de ataque melee y si el enemigo puede atacar
        if (inMeleeRange && canAttack)
        {
            // Realiza un ataque melee al jugador
            StartCoroutine(AttackCooldown());
        }
        else
        {
            // Si no está atacando, corre
            isRunning = true;
            isAttacking = false;
            animator.SetBool("IsRunning", isRunning);
            animator.SetBool("IsAttacking", isAttacking);
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false; // Desactiva la capacidad de ataque durante el cooldown
        AttackMelee(); // Realiza el ataque melee
        yield return new WaitForSeconds(2f); // Espera dos segundos antes de permitir otro ataque
        canAttack = true; // Permite el siguiente ataque después del cooldown
    }

    private void AttackMelee()
    {
        // Realiza un ataque cuerpo a cuerpo al jugador
        Debug.Log("Enemy realiza un ataque cuerpo a cuerpo al jugador");
        playerHealth.Damage((int)meleeAttackDamage);
        // Cambia la animación a ataque
        isRunning = false;
        isAttacking = true;
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsAttacking", true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MeleeAttackTrigger"))
        {
            inMeleeRange = true;
        }
        else if (other.CompareTag("Arrow"))
        {
            StartCoroutine(GetDamage());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MeleeAttackTrigger"))
        {
            inMeleeRange = false;
        }
    }

    private void OnMouseDown()
    {
        CombatBehaviour combatBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<CombatBehaviour>();
        if (combatBehaviour != null && combatBehaviour.IsMeleeMode() && inMeleeRange)
        {
            StartCoroutine(GetDamage());
        }
    }

    public IEnumerator GetDamage()
    {
        float damageDuration = 0.1f;
        float damage = UnityEngine.Random.Range(1f, 5f);
        health -= damage;
        barraDeVidaOrco.UpdateHealthBar(maxHealth, health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        // No es necesario cambiar el color si el enemigo sigue vivo
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(damageDuration);
        // No es necesario cambiar el color si el enemigo sigue vivo
        spriteRenderer.color = Color.white;
    }
}

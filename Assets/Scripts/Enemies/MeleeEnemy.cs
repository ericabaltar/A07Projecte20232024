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
        // Verifica si el jugador est� dentro del rango de ataque melee y si el enemigo puede atacar
        if (Vector2.Distance(transform.position, playerHealth.transform.position) <= meleeAttackRange && canAttack)
        {
            // Realiza un ataque melee al jugador
            StartCoroutine(AttackCooldown());
        }
        else
        {
            // Si no est� atacando, corre
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
        canAttack = true; // Permite el siguiente ataque despu�s del cooldown
    }

    private void AttackMelee()
    {
        // Realiza un ataque cuerpo a cuerpo al jugador
        Debug.Log("Enemy realiza un ataque cuerpo a cuerpo al jugador");
        playerHealth.Damage((int)meleeAttackDamage);
        // Cambia la animaci�n a ataque
        isRunning = false;
        isAttacking = true;
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsAttacking", true);
    }

    private void OnMouseDown()
    {
        StartCoroutine(GetDamage());
    }

    public IEnumerator GetDamage()
    {
        float damageDuration = 0.1f;
        float damage = UnityEngine.Random.Range(1f, 5f);
        health -= damage;
        barraDeVidaOrco.UpdateHealthBar(maxHealth, health);

        if (health > 0)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(damageDuration);
            spriteRenderer.color = Color.white;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
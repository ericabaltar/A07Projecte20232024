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
    [SerializeField] private AudioClip attackSound; // Sonido de ataque del orco
    [SerializeField] private AudioClip OrcoSoundDamage;
    [SerializeField] private AudioClip OrcoSoundDead;
    private ControladorSonido controladorSonido; // Referencia al ControladorSonido
    private AudioSource audioSource;
    private CanvasGroup canvasGroup;
    private float alphaTimer = 0f;
    private float holdDuration = 0.5f;
    private bool AlphaChanged = false;
    private void Start()
    {
        health = maxHealth;
        barraDeVidaOrco.UpdateHealthBar(maxHealth, health);
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBehaviour>();
        animator = GetComponent<Animator>();
        controladorSonido = ControladorSonido.Instance; // Obtener la instancia del ControladorSonido
        audioSource = GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    private void Update()
    {
        // Verifica si el jugador est� dentro del rango de ataque melee y si el enemigo puede atacar
        if (inMeleeRange && canAttack)
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
        if (AlphaChanged)
        {
            alphaTimer += Time.deltaTime;
            if (alphaTimer >= holdDuration)
            {
                canvasGroup.alpha = 0;
                AlphaChanged = false;
            }
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
        // Reproduce el sonido de ataque del orco si est� asignado y hay un ControladorSonido
        if (attackSound != null && controladorSonido != null)
        {
            controladorSonido.EjecutadorDeSonido(attackSound);
        }

        // Realiza un ataque cuerpo a cuerpo al jugador
        Debug.Log("Enemy realiza un ataque cuerpo a cuerpo al jugador");
        playerHealth.Damage((int)meleeAttackDamage);
        // Cambia la animaci�n a ataque
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
        canvasGroup.alpha = 1;
        AlphaChanged = true;
        alphaTimer = 0f;
        float damageDuration = 0.1f;
        float damage = UnityEngine.Random.Range(1f, 5f);
        health -= damage;
        barraDeVidaOrco.UpdateHealthBar(maxHealth, health);

        if (health <= 0)
        {
            controladorSonido.EjecutadorDeSonido(OrcoSoundDead);
            Destroy(gameObject);
        }

        // No es necesario cambiar el color si el enemigo sigue vivo
        spriteRenderer.color = Color.red;
        controladorSonido.EjecutadorDeSonido(OrcoSoundDamage);
        yield return new WaitForSeconds(damageDuration);
        // No es necesario cambiar el color si el enemigo sigue vivo
        spriteRenderer.color = Color.white;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemy : MonoBehaviour
{
    [SerializeField] private float rangedAttackDamage = 10f;
    [SerializeField] private float rangedAttackRange = 5f;
    [SerializeField] private float followRange = 10f;
    [SerializeField] private float desiredDistance = 3f;
    [SerializeField] private float maxHealth;
    [SerializeField] private BarraDeVidaOrco barraDeVidaOrco;
    [SerializeField] private ParticleSystem magicAttackParticles;
    [SerializeField] private AudioClip attackSound; // Sonido de ataque
    [SerializeField] private AudioClip magicAttackSound; // Sonido de las partículas de ataque
    [SerializeField] private AudioClip SkeletonSoundDamage; 
    [SerializeField] private AudioClip SkeletonSoundDead; 
    private HealthBehaviour playerHealth;
    private NavMeshAgent navMeshAgent;
    private bool canAttack = true;
    public float health;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool inMeleeRange = false;
    private ControladorSonido controladorSonido; // Referencia al ControladorSonido
    private AudioSource AudioSource;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        health = maxHealth;
        barraDeVidaOrco.UpdateHealthBar(maxHealth, health);
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBehaviour>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        animator = GetComponent<Animator>();
        controladorSonido = ControladorSonido.Instance; // Obtener la instancia del ControladorSonido
        AudioSource = GetComponent<AudioSource>();
        AudioSource = GetComponent<AudioSource>();
        canvasGroup = GetComponentInChildren<CanvasGroup>();

        magicAttackParticles.Stop();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerHealth.transform.position);

        if (distanceToPlayer <= rangedAttackRange && canAttack)
        {
            StartCoroutine(RangedAttackCooldown());
            animator.SetBool("IsAttacking", true);
            animator.SetBool("IsRunning", false);
        }
        else if (distanceToPlayer <= followRange)
        {
            if (distanceToPlayer > desiredDistance)
            {
                Vector3 directionToPlayer = playerHealth.transform.position - transform.position;
                directionToPlayer.Normalize();

                Vector3 desiredPosition = playerHealth.transform.position - directionToPlayer * desiredDistance;

                navMeshAgent.SetDestination(desiredPosition);
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsAttacking", false);
            }
            else
            {
                navMeshAgent.SetDestination(transform.position);
            }
        }
        else
        {
            navMeshAgent.SetDestination(transform.position);
        }
    }

    private IEnumerator RangedAttackCooldown()
    {
        canAttack = false;
        AttackRanged();
        yield return new WaitForSeconds(3f);
        canAttack = true;
    }

    private void AttackRanged()
    {
        if (attackSound != null && controladorSonido != null)
        {
            controladorSonido.EjecutadorDeSonido(attackSound); // Reproducir el sonido de ataque utilizando el ControladorSonido
        }

        Vector3 playerPosition = playerHealth.transform.position;

        Vector3 particlePosition = playerPosition + new Vector3(0f, 1f, 0f);

        magicAttackParticles.transform.position = particlePosition;

        magicAttackParticles.Play();

        if (magicAttackSound != null && controladorSonido != null) // magicAttackSound sería tu AudioClip para el sonido de las partículas
        {
            controladorSonido.EjecutadorDeSonido(magicAttackSound); // Reproducir el sonido de las partículas utilizando el ControladorSonido
        }

        playerHealth.Damage((int)rangedAttackDamage);
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
        float damageDuration = 0.1f;
        float damage = UnityEngine.Random.Range(1f, 5f);
        health -= damage;
        barraDeVidaOrco.UpdateHealthBar(maxHealth, health);

        if (health > 0)
        {
            spriteRenderer.color = Color.red;
            if(SkeletonSoundDamage != null && AudioSource != null)
            {

                controladorSonido.EjecutadorDeSonido(SkeletonSoundDamage);
            }
            yield return new WaitForSeconds(damageDuration);
            spriteRenderer.color = Color.white;
        }
        else
        {
            if (SkeletonSoundDead != null && AudioSource != null)
            {
                controladorSonido.EjecutadorDeSonido(SkeletonSoundDead);
            }
            Destroy(gameObject);
        }
    }
}

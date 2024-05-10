using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    [Header("Melee Attack Settings")]
    [SerializeField] private float meleeAttackDamage = 10f;
    [SerializeField] private float meleeAttackRange = 1.5f;
    [Header("Ranged Attack Settings")]
    [SerializeField] private float rangedAttackDamage = 10f;
    [SerializeField] private float rangedAttackRange = 5f;
    [SerializeField] private float followRange = 10f;
    [SerializeField] private float desiredDistance = 3f;
    [Header("General Settings")]
    [SerializeField] private float maxHealth;
    [SerializeField] private BarraDeVidaOrco barraDeVidaOrco;
    [SerializeField] private ParticleSystem magicAttackParticles;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private AudioClip BossSoundDamage;
    [SerializeField] private AudioClip BossSoundDead;
    private HealthBehaviour playerHealth;
    private NavMeshAgent navMeshAgent;
    private bool canAttack = true;
    private float health;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool inMeleeRange = false;
    private ControladorSonido controladorSonido;

    private void Start()
    {
        health = maxHealth;
        barraDeVidaOrco.UpdateHealthBar(maxHealth, health);
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBehaviour>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        magicAttackParticles.Stop();
        animator = GetComponent<Animator>();
        controladorSonido = ControladorSonido.Instance;
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerHealth.transform.position);

        if (canAttack)
        {
            if (distanceToPlayer <= meleeAttackRange && inMeleeRange)
            {
                StartCoroutine(MeleeAttackCooldown());
            }
            else if (distanceToPlayer <= rangedAttackRange)
            {
                StartCoroutine(RangedAttackCooldown());
            }
        }

        if (distanceToPlayer <= followRange)
        {
            if (distanceToPlayer > desiredDistance)
            {
                Vector3 directionToPlayer = playerHealth.transform.position - transform.position;
                directionToPlayer.Normalize();
                Vector3 desiredPosition = playerHealth.transform.position - directionToPlayer * desiredDistance;
                navMeshAgent.SetDestination(desiredPosition);
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

    private IEnumerator MeleeAttackCooldown()
    {
        canAttack = false;
        Debug.Log("Boss realiza un ataque cuerpo a cuerpo al jugador");
        AttackMelee();
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void AttackMelee()
    {
        playerHealth.Damage((int)meleeAttackDamage);
        animator.SetTrigger("MeleeAttack");
    }

    private IEnumerator RangedAttackCooldown()
    {
        canAttack = false;
        Debug.Log("Boss realiza un ataque a distancia al jugador");
        AttackRanged();
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void AttackRanged()
    {
        Vector3 playerPosition = playerHealth.transform.position;
        Vector3 particlePosition = playerPosition + new Vector3(0f, 1f, 0f);
        magicAttackParticles.transform.position = particlePosition;
        magicAttackParticles.Play();
        playerHealth.Damage((int)rangedAttackDamage);
        animator.SetTrigger("RangedAttack");
    }

    private void OnMouseDown()
    {
        CombatBehaviour combatBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<CombatBehaviour>();
        if (combatBehaviour != null && combatBehaviour.IsMeleeMode() && inMeleeRange)
        {
            StartCoroutine(GetDamage());
        }
    }


    private IEnumerator GetDamage()
    {
        float damageDuration = 0.1f;
        float damage = UnityEngine.Random.Range(1f, 5f);
        health -= damage;
        barraDeVidaOrco.UpdateHealthBar(maxHealth, health);

        if (health <= 0)
        {
            animator.SetTrigger("Die");
            controladorSonido.EjecutadorDeSonido(BossSoundDead);
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
            SceneManager.LoadScene(4);
        }
        else
        {
            spriteRenderer.color = Color.red;
            controladorSonido.EjecutadorDeSonido(BossSoundDamage);
            yield return new WaitForSeconds(damageDuration);
            spriteRenderer.color = Color.white;
        }
    }

    private bool IsArrowHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        if (hit.collider != null && hit.collider.CompareTag("Arrow"))
        {
            return true;
        }
        return false;
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


}

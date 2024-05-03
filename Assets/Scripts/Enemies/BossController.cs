using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField] private float attackCooldown = 2f; // Tiempo entre ataques
    private HealthBehaviour playerHealth;
    private NavMeshAgent navMeshAgent;
    private bool canAttack = true; // Controla el cooldown entre ataques
    private float health;
    private SpriteRenderer spriteRenderer;

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
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerHealth.transform.position);

        if (canAttack)
        {
            if (distanceToPlayer <= meleeAttackRange)
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
    }

    private void OnMouseDown()
    {
        StartCoroutine(GetDamage());
    }

    private IEnumerator GetDamage()
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

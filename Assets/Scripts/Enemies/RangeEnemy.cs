using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemy : MonoBehaviour
{
    [SerializeField] private float rangedAttackDamage = 10f;
    [SerializeField] private float rangedAttackRange = 5f;
    [SerializeField] private float followRange = 10f; // Rango de seguimiento al jugador
    [SerializeField] private float desiredDistance = 3f; // Distancia deseada respecto al jugador
    [SerializeField] private float maxHealth;
    [SerializeField] private BarraDeVidaOrco barraDeVidaOrco;
    [SerializeField] private ParticleSystem magicAttackParticles; // Referencia al sistema de partículas de ataque mágico
    private HealthBehaviour playerHealth;
    private NavMeshAgent navMeshAgent;
    private bool canAttack = true; // Variable para controlar el cooldown
    public float health;
    private SpriteRenderer spriteRenderer;
    private Animator animator; // Referencia al componente Animator

    private void Start()
    {
        health = maxHealth;
        barraDeVidaOrco.UpdateHealthBar(maxHealth, health);
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthBehaviour>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        animator = GetComponent<Animator>(); // Obtener el componente Animator del objeto

        // Desactiva el sistema de partículas de ataque mágico al inicio
        magicAttackParticles.Stop();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerHealth.transform.position);

        if (distanceToPlayer <= rangedAttackRange && canAttack)
        {
            // Realiza un ataque a distancia al jugador
            StartCoroutine(RangedAttackCooldown());
            // Cambia el estado de animación a "IsAttacking"
            animator.SetBool("IsAttacking", true);
            animator.SetBool("IsRunning", false);
        }
        else if (distanceToPlayer <= followRange)
        {
            if (distanceToPlayer > desiredDistance) // Agrega esta condición para verificar si la distancia al jugador es mayor que la distancia deseada
            {
                // Calcula el vector que va desde el enemigo hasta el jugador
                Vector3 directionToPlayer = playerHealth.transform.position - transform.position;
                directionToPlayer.Normalize();

                // Calcula el punto que está a la distancia deseada del jugador en la dirección opuesta
                Vector3 desiredPosition = playerHealth.transform.position - directionToPlayer * desiredDistance;

                // Mueve al enemigo hacia el punto deseado
                navMeshAgent.SetDestination(desiredPosition);
                // Cambia el estado de animación a "IsRunning"
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsAttacking", false);
            }
            else
            {
                // Detiene al enemigo si está dentro de la distancia deseada
                navMeshAgent.SetDestination(transform.position);
                // No se establece ningún estado aquí porque no queremos que haya un estado "Idle"
            }
        }
        else
        {
            // Detiene al enemigo si está fuera del rango de seguimiento
            navMeshAgent.SetDestination(transform.position);
            // No se establece ningún estado aquí porque no queremos que haya un estado "Idle"
        }
    }


    private IEnumerator RangedAttackCooldown()
    {
        canAttack = false; // Desactiva la capacidad de ataque durante el cooldown
        AttackRanged(); // Realiza el ataque a distancia
        yield return new WaitForSeconds(3f); // Espera dos segundos antes de permitir otro ataque
        canAttack = true; // Permite el siguiente ataque después del cooldown
    }

    private void AttackRanged()
    {
        // Obtener la posición del jugador
        Vector3 playerPosition = playerHealth.transform.position;

        // Ajustar la posición relativa para que esté más arriba del jugador
        Vector3 particlePosition = playerPosition + new Vector3(0f, 1f, 0f); // Ajusta el valor Y según sea necesario

        // Asignar la posición ajustada al sistema de partículas de ataque mágico
        magicAttackParticles.transform.position = particlePosition;

        // Activar el sistema de partículas de ataque mágico
        magicAttackParticles.Play();

        // Daña al jugador al atacar
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

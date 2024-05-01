using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemy : MonoBehaviour
{
    [SerializeField] private float rangedAttackDamage = 10f;
    [SerializeField] private float rangedAttackRange = 5f;
    [SerializeField] private GameObject bulletPrefab; // Prefab de la bala
    [SerializeField] private Transform firePoint; // Punto desde el que se instanciará la bala
    [SerializeField] private float bulletSpeed = 10f; // Variable para controlar la velocidad de la bala
    [SerializeField] private float followRange = 10f; // Rango de seguimiento al jugador
    [SerializeField] private float desiredDistance = 3f; // Distancia deseada respecto al jugador
    [SerializeField] private float maxHealth;
    [SerializeField] private BarraDeVidaOrco barraDeVidaOrco;
    private HealthBehaviour playerHealth;
    private NavMeshAgent navMeshAgent;
    private bool canAttack = true; // Variable para controlar el cooldown
    public float health;
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
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerHealth.transform.position);

        if (distanceToPlayer <= rangedAttackRange && canAttack)
        {
            // Realiza un ataque a distancia al jugador
            StartCoroutine(RangedAttackCooldown());
        }
        else if (distanceToPlayer <= followRange)
        {
            // Calcula el vector que va desde el enemigo hasta el jugador
            Vector3 directionToPlayer = playerHealth.transform.position - transform.position;
            directionToPlayer.Normalize();

            // Calcula el punto que está a la distancia deseada del jugador en la dirección opuesta
            Vector3 desiredPosition = playerHealth.transform.position - directionToPlayer * desiredDistance;

            // Mueve al enemigo hacia el punto deseado
            navMeshAgent.SetDestination(desiredPosition);
        }
        else
        {
            // Detiene al enemigo si está fuera del rango de seguimiento
            navMeshAgent.SetDestination(transform.position);
        }
    }

    private IEnumerator RangedAttackCooldown()
    {
        canAttack = false; // Desactiva la capacidad de ataque durante el cooldown
        AttackRanged(); // Realiza el ataque a distancia
        yield return new WaitForSeconds(2f); // Espera dos segundos antes de permitir otro ataque
        canAttack = true; // Permite el siguiente ataque después del cooldown
    }

    private void AttackRanged()
    {
        // Instancia una bala desde el prefab en el punto de fuego
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>(); // Obtener el Rigidbody de la bala
        bulletRb.velocity = firePoint.right * bulletSpeed; // Aplicar velocidad a la bala
        playerHealth.Damage((int)rangedAttackDamage); // Daña al jugador al atacar
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

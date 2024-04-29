using UnityEngine;

public class Boss : MonoBehaviour
{
    // Comportamiento del jefe
    public float meleeAttackRange = 1.5f;
    public float rangedAttackRange = 5f;
    public float meleeAttackDamage = 10f;
    public float rangedAttackDamage = 5f;
    public float attackCooldown = 2f;
    public int attacksBeforeBurst = 3;
    public GameObject bulletPrefab;
    public Transform firePoint;

    // Componentes y variables de estado
    private Transform player;
    private bool isPlayerInRange;
    private float nextAttackTime;
    private int attacksCount;
    private HealthBehaviour playerHealth;

    private void Start()
    {
        // Encuentra el GameObject del jugador y obtén su componente HealthBehaviour
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<HealthBehaviour>();
    }

    private void Update()
    {
        // Verifica si el jugador está en el rango de ataque y si es hora de atacar
        if (isPlayerInRange)
        {
            if (Time.time >= nextAttackTime)
            {
                // Decide si atacar a melee o a distancia, dependiendo de la distancia al jugador
                if (Vector2.Distance(transform.position, player.position) <= meleeAttackRange)
                {
                    AttackMelee();
                }
                else if (Vector2.Distance(transform.position, player.position) <= rangedAttackRange)
                {
                    AttackRanged();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Activa el estado de que el jugador está en rango cuando entra en el área de activación
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Desactiva el estado de que el jugador está en rango cuando sale del área de activación
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void AttackMelee()
    {
        // Realiza un ataque cuerpo a cuerpo al jugador y actualiza el tiempo de próximo ataque
        Debug.Log("Boss realiza un ataque cuerpo a cuerpo al jugador");
        playerHealth.Damage((int)meleeAttackDamage);

        attacksCount++;
        if (attacksCount >= attacksBeforeBurst)
        {
            // Realiza una ráfaga de ataques cuerpo a cuerpo si es el momento adecuado
            for (int i = 0; i < 3; i++)
            {
                playerHealth.Damage((int)meleeAttackDamage);
                Debug.Log("Boss realiza un ataque cuerpo a cuerpo al jugador (Ráfaga)");
            }
            attacksCount = 0;
        }
        nextAttackTime = Time.time + attackCooldown;
    }

    private void AttackRanged()
    {
        // Realiza un ataque a distancia al jugador y actualiza el tiempo de próximo ataque
        Debug.Log("Boss realiza un ataque a distancia al jugador");
        playerHealth.Damage((int)rangedAttackDamage);

        attacksCount++;
        if (attacksCount >= attacksBeforeBurst)
        {
            // Realiza una ráfaga de ataques a distancia si es el momento adecuado
            for (int i = 0; i < 3; i++)
            {
                playerHealth.Damage((int)rangedAttackDamage);
                Debug.Log("Boss realiza un ataque a distancia al jugador (Ráfaga)");
            }
            attacksCount = 0;
        }
        nextAttackTime = Time.time + attackCooldown;
    }
}

using UnityEngine;

public class CombatBehaviour : MonoBehaviour
{
    public BoxCollider2D meleeAttackTrigger;
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public GameObject bow;

    public bool melee = true;
    public bool hasBow = false;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer bowSpriteRenderer;
    private Animator animator; // Agregar esta variable

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        bowSpriteRenderer = bow.GetComponent<SpriteRenderer>();
        // Ocultar el arco al inicio
        bowSpriteRenderer.enabled = false;
        animator = GetComponent<Animator>(); // Obtener el componente Animator
    }

    void Update()
    {
        if (!hasBow)
        {
            Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(1f, 1f), 0f);
            foreach (Collider2D hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Bow"))
                {
                    hasBow = true;
                    melee = false; // Cambio de modo cuando se recoge el arco
                    bowSpriteRenderer.enabled = true; // Mostrar arco al recogerlo
                    Debug.Log("Arco recogido");
                    Destroy(hitCollider.gameObject); // Destruir el objeto "Bow"
                    break;
                }
            }
        }

        // Cambio de modo solo si se tiene el arco y se presiona la tecla de espacio
        if (hasBow && Input.GetKeyDown(KeyCode.Space))
        {
            melee = !melee;
            Debug.Log("Modo de ataque cambiado a " + (melee ? "Melee" : "Distancia"));
            // Activar o desactivar el arco según el modo de ataque
            bowSpriteRenderer.enabled = !melee;
        }

        if (hasBow && Input.GetMouseButtonDown(0))
        {
            if (melee)
            {
                PerformMeleeAttack();
                animator.SetTrigger("MeleeAttack"); // Activar la animación de ataque cuerpo a cuerpo
            }
            else
            {
                PerformRangedAttack();
            }
        }

        if (!melee)
        {
            RotateBowTowardsMouse();
        }
    }

    public bool IsMeleeMode()
    {
        return melee;
    }

    void RotateBowTowardsMouse()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mouseWorldPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bow.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void PerformMeleeAttack()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(meleeAttackTrigger.bounds.center, meleeAttackTrigger.bounds.size, 0f);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                // Verificar si el enemigo está dentro del área del trigger
                if (meleeAttackTrigger.bounds.Contains(hitCollider.transform.position))
                {
                    Debug.Log("Realizando ataque cuerpo a cuerpo a " + hitCollider.name);
                    // Lógica de daño cuerpo a cuerpo
                }
            }
        }
    }

    void PerformRangedAttack()
    {
        if (arrowPrefab != null && arrowSpawnPoint != null)
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, bow.transform.rotation);
            Destroy(arrow, 50f);
            Debug.Log("Disparando flecha");
        }
        else
        {
            Debug.LogWarning("Prefab de flecha o punto de spawn no asignado en el inspector.");
        }
    }

    // Visualización del rango de ataque cuerpo a cuerpo en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(meleeAttackTrigger.bounds.center, meleeAttackTrigger.bounds.size);
    }
}

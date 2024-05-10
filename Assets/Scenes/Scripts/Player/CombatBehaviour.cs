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
    private Animator animator;
    private AudioSource audioSource; // Agregar esta variable
    public AudioClip tensarArcoSound; // Sonido de tensar el arco
    public AudioClip cambioAtaqueSound; // Sonido de cambio de ataque

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        bowSpriteRenderer = bow.GetComponent<SpriteRenderer>();
        // Ocultar el arco al inicio
        bowSpriteRenderer.enabled = false;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Obtener el componente AudioSource
        audioSource.clip = tensarArcoSound; // Asignar el clip de audio al AudioSource
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
                    audioSource.PlayOneShot(cambioAtaqueSound); // Reproducir sonido de cambio de ataque
                    break;
                }
            }
        }

        // Cambio de modo solo si se tiene el arco y se presiona la tecla de espacio
        if (hasBow && Input.GetKeyDown(KeyCode.Space))
        {
            melee = !melee;
            Debug.Log("Modo de ataque cambiado a " + (melee ? "Melee" : "Distancia"));
            // Activar o desactivar el arco seg?n el modo de ataque
            bowSpriteRenderer.enabled = !melee;
            audioSource.PlayOneShot(cambioAtaqueSound); // Reproducir sonido de cambio de ataque
        }

        if (hasBow && Input.GetMouseButtonDown(0))
        {
            if (melee)
            {
                PerformMeleeAttack();
                animator.SetTrigger("MeleeAttack"); // Activar la animaci?n de ataque cuerpo a cuerpo
            }
            else
            {
                PerformRangedAttack();
                audioSource.PlayOneShot(tensarArcoSound); // Reproducir el sonido de tensar el arco
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
                // Verificar si el enemigo est? dentro del ?rea del trigger
                if (meleeAttackTrigger.bounds.Contains(hitCollider.transform.position))
                {
                    Debug.Log("Realizando ataque cuerpo a cuerpo a " + hitCollider.name);
                    // L?gica de da?o cuerpo a cuerpo
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

    // Visualizaci?n del rango de ataque cuerpo a cuerpo en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(meleeAttackTrigger.bounds.center, meleeAttackTrigger.bounds.size);
    }
}


using UnityEngine;

public class MovePJ : MonoBehaviour
{
    public float _velocidadCaminar;

    private SpriteRenderer _spriteRendererPersonaje;
    private Animator animator;
    private CombatBehaviour combatBehaviour;

    void Start()
    {
        _spriteRendererPersonaje = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Obtener la referencia al script CombatBehaviour
        combatBehaviour = GetComponent<CombatBehaviour>();
    }

    void Update()
    {
        //Trobar costats pantalla
        Vector2 minPantalla = Camera.main.ViewportToWorldPoint(new Vector2(x: 0, y: 0));
        Vector2 maxPantalla = Camera.main.ViewportToWorldPoint(new Vector2(x: 1, y: 1));

        minPantalla.x += 0.6f;
        maxPantalla.x -= 0.6f;
        minPantalla.y += 0.6f;
        maxPantalla.y -= 0.6f;

        float direccioX = Input.GetAxisRaw("Horizontal");
        float direccioY = Input.GetAxisRaw("Vertical");
        Vector2 direccioIndicada = new Vector2(direccioX, direccioY).normalized;
        GetComponent<Rigidbody2D>().velocity = direccioIndicada * _velocidadCaminar;

        // Si la magnitud del vector de dirección es mayor que cero, el personaje está en movimiento
        bool isMoving = direccioIndicada.magnitude > 0;

        _spriteRendererPersonaje.flipX = direccioX < 0;

        // Acceder a las variables melee y hasBow del script CombatBehaviour
        bool melee = combatBehaviour.IsMeleeMode();
        bool hasBow = combatBehaviour.hasBow;

        // Establecer el parámetro IsRunning basado en si está corriendo (ya sea con arco o melee)
        animator.SetBool("IsRunning", isMoving);

        // Establecer el parámetro IsBowRunning basado en si tiene el arco y está corriendo
        animator.SetBool("IsBowRunning", hasBow && isMoving);

        // Establecer el trigger MeleeAttack cuando se realiza un ataque cuerpo a cuerpo
        if (melee && Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("MeleeAttack");
        }


        if (hasBow)
        {
            animator.SetBool("IsBow", true); // Establecer el parámetro IsBow en true si se tiene el arco
        }
        else
        {
            animator.SetBool("IsBow", false); // Establecer el parámetro IsBow en false si no se tiene el arco
        }

        if (melee)
        {
            animator.SetBool("IsIdle", true); // Establecer el parámetro IsIdle en true si se está en modo de ataque cuerpo a cuerpo
        }
        else
        {
            animator.SetBool("IsIdle", false); // Establecer el parámetro IsIdle en false si no se está en modo de ataque cuerpo a cuerpo
        }
    }
}

using UnityEngine;

public class MovePJ : MonoBehaviour
{
    public float _velocidadCaminar;
    public AudioClip movimientoSound; // Sonido de movimiento del personaje

    private SpriteRenderer _spriteRendererPersonaje;
    private Animator animator;
    private CombatBehaviour combatBehaviour;
    private Rigidbody2D rb2d;
    private AudioSource audioSource;
    private bool isMoving = false;
    private Vector2 ultimaDireccion; // Variable para almacenar la última dirección de movimiento

    void Start()
    {
        _spriteRendererPersonaje = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        combatBehaviour = GetComponent<CombatBehaviour>();
        rb2d = GetComponent<Rigidbody2D>();

        // Obtener la referencia al AudioSource y configurarlo
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = movimientoSound;
        audioSource.loop = true; // Repetir el sonido en un bucle
        audioSource.playOnAwake = false; // No reproducir el sonido automáticamente al iniciar

        ultimaDireccion = Vector2.right; // Dirección inicial (derecha)
    }

    void Update()
    {
        //Trobar costats pantalla
        Vector2 minPantalla = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxPantalla = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        minPantalla.x += 0.6f;
        maxPantalla.x -= 0.6f;
        minPantalla.y += 0.6f;
        maxPantalla.y -= 0.6f;

        float direccioX = Input.GetAxisRaw("Horizontal");
        float direccioY = Input.GetAxisRaw("Vertical");
        Vector2 direccioIndicada = new Vector2(direccioX, direccioY).normalized;
        rb2d.velocity = direccioIndicada * _velocidadCaminar;

        // Si la magnitud del vector de dirección es mayor que cero, el personaje está en movimiento
        isMoving = direccioIndicada.magnitude > 0;

        // Actualizar la última dirección solo si hay movimiento
        if (isMoving)
        {
            ultimaDireccion = direccioIndicada;
        }

        // Flip el sprite basado en la última dirección
        if (ultimaDireccion.x < 0)
        {
            _spriteRendererPersonaje.flipX = true;
        }
        else if (ultimaDireccion.x > 0)
        {
            _spriteRendererPersonaje.flipX = false;
        }

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

        // Reproducir o detener el sonido de movimiento según el estado de movimiento del personaje
        if (isMoving && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (!isMoving && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}

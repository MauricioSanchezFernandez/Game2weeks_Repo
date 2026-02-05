using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D playerRb;

    [Header("Configuation Movement")]
    float movementHorizontal = 0f;
    [SerializeField] float speedMovement;
    [Range(0, 0.5f)][SerializeField] float smoothMovement;
    // La variable 'speed' es necesaria para SmoothDamp, aunque no la toquemos directamente.
    Vector3 speed = Vector3.zero;
    bool isFacingRight = true;


    [Header("Configuation Jump")]
    [SerializeField] float powerJump;
    [SerializeField] LayerMask layerGround;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector3 radiusBox;
    [SerializeField] bool isGrounded;
    bool CamJump = false;

    [Header("Configuation Dash")]
    [SerializeField] float powerDash;
    [SerializeField] float timeDash;
    float originalGravity;
    bool canDash = true;
    bool canMove = true;
    [SerializeField] float CooldownDash;
    [SerializeField] TrailRenderer trailRenderer;


    [Header("Configuation Animation")]
    Animator anim;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        // Verificamos si el trailRenderer está asignado para evitar errores
        if (trailRenderer == null)
        {
            Debug.LogWarning("Trail Renderer no asignado en PlayerController!");
        }
        anim = GetComponent<Animator>();
        originalGravity = playerRb.gravityScale;
    }

    private void Update()
    {
        Jump();
        Dash();
        // Atack(); // Descomentar si implementas la función
        // Interact(); // Descomentar si implementas la función
    }

    private void FixedUpdate()
    {
        // Usamos Physics2D.OverlapBox para detectar el suelo
        isGrounded = Physics2D.OverlapBox(groundCheck.position, radiusBox, 0f, layerGround);

        if (canMove)
        {
            // CORRECCIÓN 1: Pasar 'movementHorizontal' directamente. 
            // El Time.fixedDeltaTime no es necesario aquí, ya se gestiona internamente.
            Movement(movementHorizontal, CamJump);
        }

        CamJump = false;
    }

    //Inputs (comentario movido para mejor estructura)

    private void Movement(float mover, bool saltar)
    {
        // Movemos el personaje suavemente
        Vector3 velocidadObjetivo = new Vector2(mover, playerRb.linearVelocity.y);
        playerRb.linearVelocity = Vector3.SmoothDamp(playerRb.linearVelocity, velocidadObjetivo, ref speed, smoothMovement);

        // Giramos el personaje si es necesario
        if (mover > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (mover < 0 && isFacingRight)
        {
            Flip();
        }

        // Gestionamos el salto
        if (isGrounded && saltar)
        {
            isGrounded = false;
            playerRb.AddForce(new Vector2(0f, powerJump), ForceMode2D.Impulse); // Usar ForceMode2D.Impulse para saltos instantáneos
        }
    }

    void Jump()
    {
        movementHorizontal = Input.GetAxisRaw("Horizontal") * speedMovement;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            CamJump = true;
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dashc());
        }
    }

    // Funciones Atack e Interact (vacías, no necesitan corrección)
    private void Atack()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Lógica de ataque aquí
        }
    }

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Lógica de interacción aquí
        }
    }


    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }


    // Corrutina Dash (CORREGIDA)
    IEnumerator Dashc()
    {
        canMove = false;
        canDash = false;
        playerRb.gravityScale = 0; // Desactivar gravedad durante el dash para un movimiento horizontal puro

        // CORRECCIÓN 2: Activar el TrailRenderer ANTES del movimiento
        if (trailRenderer != null)
        {
            trailRenderer.emitting = true;
        }

        // Aplicamos la velocidad del dash
        playerRb.linearVelocity = new Vector2(powerDash * transform.localScale.x, 0);

        // Esperamos la duración del dash
        yield return new WaitForSeconds(timeDash);

        // Volvemos a la normalidad
        if (trailRenderer != null)
        {
            trailRenderer.emitting = false;
        }
        canMove = true;
        playerRb.gravityScale = originalGravity;

        // Esperamos el cooldown para poder hacer otro dash
        yield return new WaitForSeconds(CooldownDash);
        canDash = true;
    }


    // Opcional: Para visualizar la caja de colisión del suelo en el Editor
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            // Dibuja un cubo para representar el OverlapBox
            Gizmos.DrawCube(groundCheck.position, radiusBox);
        }
    }
}

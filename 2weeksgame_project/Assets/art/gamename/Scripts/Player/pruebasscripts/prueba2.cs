using System.Collections;
using UnityEngine;

public class prueba2 : MonoBehaviour
{
    Rigidbody2D playerRb;

    [Header("Movimiento")]
    float movimientohorizontal = 0f;
    [SerializeField] float velocidadMovimiento;
    [Range(0, 0.5f)][SerializeField] float suavizadoMovimiento;
    Vector3 velocidad = Vector3.zero;
    bool mirandoDerecha = true;


    [Header("Salto")]
    [SerializeField] float fuerzaSalto;
    [SerializeField] LayerMask queesSuelo;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector3 domensionescaja;
    [SerializeField] bool enSuelo;
    bool salto = false;

    [Header("Dash")]
    [SerializeField] float velocidadDash;
    [SerializeField] float tiempoDash;
    float gravedadInicial;
    bool puedeHacerDash = true;
    bool sePuedeMover = true;
    [SerializeField] TrailRenderer trailRenderer;


    [Header("Animacion")]
    Animator animator;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gravedadInicial = playerRb.gravityScale;
    }

    private void Update()
    {
        movimientohorizontal = Input.GetAxisRaw("Horizontal") * velocidadMovimiento;
        if (Input.GetButtonDown("Jump"))
        {
            salto = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && puedeHacerDash)
        {

            StartCoroutine(Dashc());

        }

    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(groundCheck.position, domensionescaja, 0f, queesSuelo);

        if (sePuedeMover)
        {

            Mover(movimientohorizontal * Time.fixedDeltaTime, salto);

        }


        salto = false;
    }

    private void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, playerRb.linearVelocity.y);
        playerRb.linearVelocity = Vector3.SmoothDamp(playerRb.linearVelocity, velocidadObjetivo, ref velocidad, suavizadoMovimiento);

        if (mover > 0 && !mirandoDerecha)
        {

            Girar();


        }


        else if (mover < 0 && mirandoDerecha)
        {

            Girar();
        }

        if (enSuelo && saltar)
        {
            enSuelo = false;
            playerRb.AddForce(new Vector2(0f, fuerzaSalto));

        }

    }
    IEnumerator Dashc()
    {

        sePuedeMover = false;
        puedeHacerDash = false;
        playerRb.linearVelocity = new Vector2(velocidadDash * transform.localScale.x, 0);
        yield return new WaitForSeconds(tiempoDash);
        trailRenderer.emitting = true;
        sePuedeMover = true;
        puedeHacerDash = true;
        playerRb.gravityScale = gravedadInicial;
        trailRenderer.emitting = false;

    }




    void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;

    }

}

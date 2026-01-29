using UnityEngine;

public class pruebas : MonoBehaviour
{

    Rigidbody2D playerRb;

    [Header("Movimiento")]
    float movimientohorizontal = 0f;
    [SerializeField] float velocidadMovimiento;
    [Range (0, 0.5f)][SerializeField] float suavizadoMovimiento;
    Vector3 velocidad = Vector3.zero;
    bool mirandoDerecha = true;


    [Header("Salto")]
    [SerializeField] float fuerzaSalto;
    [SerializeField] LayerMask queesSuelo;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector3 domensionescaja;
    [SerializeField] bool enSuelo;
    bool salto = false;


    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movimientohorizontal = Input.GetAxisRaw("Horizontal") * velocidadMovimiento;
        if (Input.GetButtonDown("Jump")) 
        { 
            salto = true;
        }
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(groundCheck.position, domensionescaja, 0f, queesSuelo);
        Mover(movimientohorizontal * Time.fixedDeltaTime, salto);

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


        void Girar()
        {
            mirandoDerecha = !mirandoDerecha;
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        
        }

        


    }

}

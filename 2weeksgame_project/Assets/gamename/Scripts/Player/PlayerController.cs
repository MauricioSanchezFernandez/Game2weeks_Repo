using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //Variables de ref
    Rigidbody2D playerRb; //almacen del rigbody del player
    float horizontalInput;


    //Variables estdisticas del player
    [Header("movement Configuration")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] bool isFacingRight; //define orientacion del personaje
    bool canMove = true; //se puede mover?

    
    [Header("Animation Configuration")]
    Animator anim; //almacen del controlador de animacion del player

    [Header("GroundCheck Configuration")]
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck; //Posicion del detector del suelo
    [SerializeField] float groundCheckRadius; //Define el radio del circulo detector de suelo
    [SerializeField] LayerMask groundLayer; //Define la capa que puede tocar el detector de suelo

    [Header("Dash Configuration")]
     bool canDash = true;  //puede dashear?
    bool isDashing = false; //esta dasheando?
       [SerializeField] float powerDash;//potencia/velocidad del dash
    [SerializeField] float timeDashing; //tiempo de dash
    [SerializeField] float cooldownDash; //cooldown del dash quien lo diria
    [SerializeField] TrailRenderer tr; //efecto dash
    [SerializeField] float gravedadInicial; 





    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>(); //autoreferencias un componente propio
        anim = GetComponent<Animator>();
        gravedadInicial = playerRb.gravityScale;
        //Movement(horizontalInput * Time.fixedDeltaTime, )
       
    }

    void Start()
    {
               isFacingRight = true;
                
    }

    
    void Update()
    {
        Movement();
        Jump();
        Dash();

        if (isDashing)
        { 
            return;
        }

        //Logica de deteccion del suelo

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        //logica flip del personaje
              
        if (horizontalInput > 0 && !isFacingRight) Flip();
        if (horizontalInput < 0 && isFacingRight) Flip();
    }

        private void FixedUpdate()
    {

        if (isDashing)
        {
            return;
        }

        Movement();
    }


    //INPUTS
    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        playerRb.linearVelocity = new Vector2(horizontalInput * speed, playerRb.linearVelocity.y);

                
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        { playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse); }
        
    
    }

    void Attack()
    {
        if (Input.GetKey(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0)) //mantenerpulsado
        {
            //configattack
        }
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Mouse1) && canDash) //pulsar1
        {
            StartCoroutine(Dashc());
        }
    }

    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.W)) //pulsar1
        {
            //config interact
        }
    }

    void Flip()
    {

        Vector3 currentScale = transform.localScale; //almacen temporal de la escala del objeto
        currentScale.x *= -1; //invertir el valor x
        transform.localScale = currentScale; //le devolvemos la escala al objeto con el valor en x inverso
       isFacingRight = !isFacingRight; //decirle al bool que cambie al valor contrario

    }

    //corrutina dash-corrutinas
    IEnumerator Dashc()
    {
        canMove = false;
        canDash = false;
        isDashing = true;
        playerRb.gravityScale = 0;
        playerRb.linearVelocity = new Vector2(transform.localScale.x * powerDash, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(timeDashing);
        tr.emitting = false;
        playerRb.gravityScale = gravedadInicial;
        isDashing = false;
        canMove = true;
        yield return new WaitForSeconds(cooldownDash);
        canDash = true;
        

    }



}



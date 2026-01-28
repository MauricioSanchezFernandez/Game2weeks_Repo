using UnityEngine;
using UnityEngine.InputSystem;

public class COMPARACION : MonoBehaviour
{
    [Header("Movement and Jump Configuration")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isFacingRight; //define orientacion del personaje
    [SerializeField] Transform groundCheck; //Posicion del detector del suelo
    [SerializeField] float groundCheckRadius; //Define el radio del circulo detector de suelo
    [SerializeField] LayerMask groundLayer; //Define la capa que puede tocar el detector de suelo


    [Header("Shoot Configuration")]
    [SerializeField] GameObject proyectile; //ref al prefav de la bala
    [SerializeField] Transform shootPoint; //ref a la posicion desde la cual se dispara
    [SerializeField] float shootCooldown = 1f;
    bool canShoot;


    //Variables de referencia general
    Rigidbody2D playerRb; //almacen del rigbody del player
    Animator anim; //almacen del controlador de animacion del player
    PlayerInput input; //almacen del controlador de inputs del player
    Vector2 moveInput; //almacen del valor de los botones de movimiento
    bool canAttack; //bool de seguridad si define si se puede atacar o no

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>(); //autoreferencias un componente propio
        anim = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        canAttack = true;
        canShoot = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Logica de deteccion del suelo

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //logica de las animaciones
        AnimationManagement();

        //logica flip del personaje
        if (moveInput.x > 0 && !isFacingRight) Flip();
        if (moveInput.x < 0 && isFacingRight) Flip();

    }



    private void FixedUpdate()
    {
        Movement();
    }


    void Movement()
    {

        playerRb.linearVelocity = new Vector2(moveInput.x * speed, playerRb.linearVelocity.y);

    }
    //mover el motor de aceleracion del rigbody

    void Flip()
    {

        Vector3 currentScale = transform.localScale; //almacen temporal de la escala del objeto
        currentScale.x *= -1; //invertir el valor x
        transform.localScale = currentScale; //le devolvemos la escala al objeto con el valor en x inverso
        isFacingRight = !isFacingRight; //decirle al bool que cambie al valor contrario

    }

    void Jump()
    {
        playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }

    void Shoot()
    {
        canShoot = false;
        GameObject actuaProyectile = Instantiate(proyectile, shootPoint.position, Quaternion.identity);
     

        Invoke(nameof(ResetShoot), shootCooldown); //se espera tanto tiempo como shootcooldown y entonces ejecuta reset shoot

    }

    void ResetShoot()
    {

        canShoot = true; //devuelve posibilidad de disparar

    }

   


    void AnimationManagement()
    {

        //accion para gestionar animaciones 
        anim.SetBool("Jmup", !isGrounded);
        if (moveInput.x != 0) anim.SetBool("Run", true);
        else anim.SetBool("Run", false);

    }

    #region Input Methods

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

    }
    public void OnJump(InputAction.CallbackContext context)
    {

        if (context.performed && isGrounded) Jump();

    }
    public void OnAttack(InputAction.CallbackContext context)
    {
 

    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        //logica del inicio del disparo
        if (context.performed && canShoot) Shoot();


    }


    #endregion

}

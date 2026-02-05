using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D playerRb;

    [Header("Configuation Movement")]
    float movementHorizontal = 0f;
    [SerializeField] float speedMovement;
    [Range(0, 0.5f)][SerializeField] float smoothMovement;
    Vector3 speed = Vector3.zero;
    bool isFacingRight = true;
    float inputX;


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


    [Header("Configuation Attack")]
    bool canAttack;
    [SerializeField] Transform Weapon;


    [Header("Configuation Slide")]
    [SerializeField] Transform wallController;
    [SerializeField] Vector3 boxDimensionSlide;
    bool inWall; //contacto con la pared
    bool inSlide; //deslizando
    [SerializeField] float speedSlide;
    //saltopared
    [SerializeField] float powerJumpWallX;
    [SerializeField] float powerJumpWallY;
    [SerializeField] float timeJumpWall;
    bool isJumpWall;



    [Header("Configuation Animation")]
    Animator anim;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalGravity = playerRb.gravityScale;
        trailRenderer.emitting = false;
        canAttack = true;
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        movementHorizontal = inputX * speedMovement;
        anim.SetFloat("Horizontal", Mathf.Abs(movementHorizontal));
        anim.SetFloat("SpeedY", playerRb.linearVelocity.y);
        anim.SetBool("Sliding", inSlide);
        Jump();
        Dash();
        Attack();
        Slice();



    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, radiusBox, 0f, layerGround);
        anim.SetBool("inGround", isGrounded);
        anim.SetBool("ButtonDown", Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D));

        inWall = Physics2D.OverlapBox(wallController.position, boxDimensionSlide, 0f, layerGround);

        if (canMove)
        {

            Movement(movementHorizontal * Time.fixedDeltaTime, CamJump);

        }


        CamJump = false;

        if (inSlide)
        {

            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, Mathf.Clamp(playerRb.linearVelocity.y, -speedSlide, float.MaxValue));

        }

    }


    //Inputs

    private void Movement(float mover, bool saltar)
    {

        if (!isJumpWall)
        {

            Vector3 velocidadObjetivo = new Vector2(mover, playerRb.linearVelocity.y);
            playerRb.linearVelocity = Vector3.SmoothDamp(playerRb.linearVelocity, velocidadObjetivo, ref speed, smoothMovement);

        }


        if (mover > 0 && !isFacingRight)
        {

            Flip();

        }


        else if (mover < 0 && isFacingRight)
        {

            Flip();
        }

        if (isGrounded && saltar)
        {
            isGrounded = false;
            playerRb.AddForce(new Vector2(0f, powerJump));

        }

    }

    void Jump()
    {


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !inSlide)
        {
            CamJump = true;
        }


        if (CamJump && inWall && inSlide)
        {
            JumpWall();

        }


    }


    void JumpWall()
    {
        inWall = false;
        playerRb.linearVelocity = new Vector2(powerJumpWallX * -inputX, powerJumpWallY);

        StartCoroutine(ChangeJumpWall());

    }


    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {

            StartCoroutine(Dashc());


        }
    }




    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;

    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.E) && isGrounded && canAttack)
        {

            StartCoroutine(Attackc());


        }
    }

    void Slice()
    {

        if (!isGrounded && inWall && inputX != 0)
        {
            inSlide = true;

        }

        else
        {
            inSlide = false;

        }
    }

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {



        }
    }

    //corrutina saltopared

    IEnumerator ChangeJumpWall()
    {
        isJumpWall = true;
        yield return new WaitForSeconds(timeJumpWall);
        isJumpWall = false;

    }


    //corrutina dash
    IEnumerator Dashc()
    {

        canMove = false;
        canDash = false;
        playerRb.gravityScale = 0;
        playerRb.linearVelocity = new Vector2(powerDash * transform.localScale.x, 0);
        anim.SetTrigger("DashANIM");
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(timeDash);
        canMove = true;
        playerRb.gravityScale = originalGravity;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(CooldownDash);
        canDash = true;

    }

    //corrutina attack


    IEnumerator Attackc()
    {
        canAttack = false; //Quitar la posibilidad de atacar
        float actualSpeed = speedMovement; //guardamos velocidad atual para devolverla luego
        speedMovement = 0; //pj se queda quieto

        yield return new WaitForSeconds(0.8f); //para por el numero de segundos en el juego
        speedMovement = actualSpeed;
        canAttack = true;
        //devolver velocidad y capacidad de ataque, acaba la corrutina
        yield return null; //devolverle un tiempo a la corrutina

    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawCube(groundCheck.position, radiusBox);
        //  Gizmos.DrawCube(wallController.position, boxDimensionSlide);

    }










}
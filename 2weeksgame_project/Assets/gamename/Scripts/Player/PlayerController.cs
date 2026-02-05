using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D playerRb;

    [Header("Configuation Movement")]
    float movementHorizontal = 0f;
    [SerializeField] float speedMovement;
    [Range(0, 0.5f)][SerializeField] float smoothMovement;
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
        anim = GetComponent<Animator>();
        originalGravity = playerRb.gravityScale;
    }

    private void Update()
    {


        Jump();
        Dash();

    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, radiusBox, 0f, layerGround);

        if (canMove)
        {

            Movement(movementHorizontal * Time.fixedDeltaTime, CamJump);

        }


        CamJump = false;
    }


    //Inputs

    private void Movement(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, playerRb.linearVelocity.y);
        playerRb.linearVelocity = Vector3.SmoothDamp(playerRb.linearVelocity, velocidadObjetivo, ref speed, smoothMovement);

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

    private void Atack()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            

        }
    }

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {



        }
    }


    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;

    }


    //corrutina dash
    IEnumerator Dashc()
    {

        canMove = false;
        canDash = false;
        playerRb.linearVelocity = new Vector2(powerDash * transform.localScale.x, 0);
        yield return new WaitForSeconds(timeDash);
        trailRenderer.emitting = true;
        canMove = true;
        playerRb.gravityScale = originalGravity;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(CooldownDash);
        canDash = true;

    }



   

   
  


   


   





}

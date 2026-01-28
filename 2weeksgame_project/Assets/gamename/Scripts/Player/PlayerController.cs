using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //Variables de ref
    Rigidbody2D playerRb; //almacen del rigbody del player
    Animator anim; //almacen del controlador de animacion del player
    float horizontalInput;


    //Variables estdisticas del player
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        Movement();
        Jump();
    }

    //INPUTS
    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        //playerRb.angularVelocity = new Vector2(horizontalInput * speed, playerRb.linearVelocity.y);

                
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
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
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Mouse1)) //pulsar1
        {
            //config dash
        }
    }

    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.W)) //pulsar1
        {
            //config interact
        }
    }

}



using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Movement and Jump Configuration")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;


    //variables de estasisticas player
    Rigidbody2D playerRb; //almacen del rigbody del player
    Animator anim; //almacen del controlador de animacion del player
    PlayerInput input; //almacen del controlador de inputs del player
    Vector2 moveInput; //almacen del valor de los botones de movimiento
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        playerRb.linearVelocity = new Vector2(moveInput.x * speed, playerRb.linearVelocity.y);
    }
    void Jump()
    {
        playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

}

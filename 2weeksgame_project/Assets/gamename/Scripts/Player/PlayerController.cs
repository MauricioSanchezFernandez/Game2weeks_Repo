using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //variables de ref
    private Rigidbody2D playerRb;
    private Animator anim;
    private float horizontalInput;

    //variabless  estadistica
    public float speed;
    public float jumpForce;


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
        horizontalInput = Input.GetAxis("Horizontal");
        playerRb.linearVelocityX= new Vector2(horizontalInput * speed, playerRb.linearVelocityX.y);
    
    }

    void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce);

    }

}






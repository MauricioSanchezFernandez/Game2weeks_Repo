using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public float move;
    public float speed;
    Rigidbody2D playerRb;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        playerRb.linearVelocity = new Vector2(move * speed, playerRb.linearVelocity.y);
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    public float speed
    private Rigidbody2d playerRb;
    private float move;

    void Start()
    {
        playerRb = GetComponent<Rigibody2D>();
        erjoesfd
    }

    
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        playerRb.linearVelocity = new Vector2(move*speed,  playerRb.linearVelocity.y);
    }
}

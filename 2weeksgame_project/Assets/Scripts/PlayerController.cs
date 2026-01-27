using UnityEngine;

public class PlayerController : MonoBehaviour 
{

    //variables de referencia-> conectado con el personaje

    //investigar por que no se pone azul
    private Rigidbody2D playerRb;
    private Animator anim;
    private float horizontalInput;


    //variables de estadisticas del player
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
        
    }
}

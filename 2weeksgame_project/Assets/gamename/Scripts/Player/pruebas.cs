using UnityEngine;

public class pruebas : MonoBehaviour
{

    Rigidbody2D playerRb;

    [Header("Movimiento")]
    float movimientohorizontal = 0f;
    [SerializeField] float velocidadMovimiento;
    [SerializeField] float suavizadoMovimiento;
    Vector3 velocidad = Vector3.zero;
    bool mirandoDerecha = true;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movimientohorizontal = Input.GetAxisRaw("Horizontal") * velocidadMovimiento;

    }

    private void FixedUpdate()
    {
        Mover();
    }

    private void Mover(float mover)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, playerRb.linearVelocity.y);
    }

}

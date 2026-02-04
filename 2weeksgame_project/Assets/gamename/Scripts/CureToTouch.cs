using UnityEngine;

public class CureToTouch : MonoBehaviour
{

    [SerializeField] int cureForTouch; //cantidad de cuaricion que puede hacer

    [SerializeField] private int cantidadCuracion;

    void OnTriggerEnter2D(Collider2D collision)
    {
       // if (collision.TryGetComponent(out VidaJugador vidaJugador))
        {
      //      vidaJugador.CurarVida(cantidadCuracion);
        }
    }





}

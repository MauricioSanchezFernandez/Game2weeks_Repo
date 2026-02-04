using UnityEngine;

public class DamageToTouch : MonoBehaviour
{

    [SerializeField] int damageForTouch; //cantidad de daño que puede hacer

    [SerializeField] private int dañoPorToque;

    void OnTriggerEnter2D(Collider2D collision)
    {
       // if (collision.TryGetComponent(out VidaJugador vidaJugador))
        {
       //     vidaJugador.TomarDaño(dañoPorToque);
        }
    }



}

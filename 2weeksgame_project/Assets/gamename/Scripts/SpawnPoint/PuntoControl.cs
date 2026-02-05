using UnityEngine;

public class PuntoControl : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))

        {
            ControlardorJuego.Instance.UltimoPuntoControl(gameObject);
        
        }
    }



}

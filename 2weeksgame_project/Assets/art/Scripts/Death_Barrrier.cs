using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    [SerializeField] float speed = 2f; // Velocidad a la que se mueve la barrera

    void Update()
    {
        // Mueve la barrera constantemente hacia la izquierda
        // Time.deltaTime asegura que el movimiento sea independiente de los FPS
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    // Esta función se llama cuando algo con IsTrigger activado entra en contacto
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprueba si el objeto que toca la barrera es el jugador
        // Para esto, el objeto Player debe tener la etiqueta "Player" (puedes añadirla en el Inspector)
        if (other.CompareTag("Player"))
        {
            Debug.Log("El jugador ha muerto!");
            // Aquí puedes añadir la lógica de muerte: reiniciar nivel, animaciones, etc.
            // Por ahora, reiniciamos la escena (requiere añadir using UnityEngine.SceneManagement; arriba del script)
            // UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}

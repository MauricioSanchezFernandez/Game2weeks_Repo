using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathHandler : MonoBehaviour
{
    public GameObject gameOverMenu; // Arrastra tu Panel aquí en el Inspector

    public void TriggerGameOver()
    {
        gameOverMenu.SetActive(true); // Muestra el menú
        Time.timeScale = 0f;          // Pausa el juego por completo
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // ¡Obligatorio! Si no, el juego seguirá pausado
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga nivel actual
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit_Menu_game : MonoBehaviour
{
    public void PlayGame()
    {
        // Carga la siguiente escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
} // <--- Esta es la llave que cierra la CLASE

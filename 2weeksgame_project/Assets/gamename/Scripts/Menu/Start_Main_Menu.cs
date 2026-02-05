using UnityEngine;
using UnityEngine.SceneManagement;

// Cambia 'MainMenu' a 'Start_Main_Menu'
public class Start_Main_Menu : MonoBehaviour
{
    public void PlayGame()
    {
        // Asegúrate de que la siguiente escena esté en File -> Build Settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

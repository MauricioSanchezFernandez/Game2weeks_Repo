using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour // La clase es obligatoria
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

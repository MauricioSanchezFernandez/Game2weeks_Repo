using UnityEngine;

public class ControlPausa : MonoBehaviour
{
    public GameObject objetoMenuPausa;
    private bool juegoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado) Reanudar();
            else Pausar();
        }
    }

    public void Pausar()
    {
        objetoMenuPausa.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;
    }

    public void Reanudar()
    {
        objetoMenuPausa.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;
    }
}

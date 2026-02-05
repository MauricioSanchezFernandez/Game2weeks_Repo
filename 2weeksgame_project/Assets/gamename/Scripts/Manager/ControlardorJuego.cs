using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlardorJuego : MonoBehaviour
{
    public static ControlardorJuego Instance;
    [SerializeField] GameObject[] puntosDeControl;
    [SerializeField] GameObject Player;
    int indexPuntosControl;

    private void Awake()
    {
        Instance = this;
        
        if (indexPuntosControl >= puntosDeControl.Length)
        {
            PlayerPrefs.SetInt("PuntosIndex", 0);
            indexPuntosControl = 0;

        }

        indexPuntosControl = PlayerPrefs.GetInt("PuntosIndex");
        Instantiate(Player, puntosDeControl[indexPuntosControl].transform.position, Quaternion.identity);
    }
    public void UltimoPuntoControl(GameObject puntoControl)
    {

        for (int i = 0; 1 < puntosDeControl.Length; i++)
            if (puntosDeControl[i] == puntoControl && i > indexPuntosControl)
            {
                PlayerPrefs.SetInt("PuntosIndex", i);
            }
    
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        }
    }

}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    static GameManager Instance;

    [SerializeField] GameObject[] spawnPoints; //puntos de respawn
    [SerializeField] GameObject Player;

    int indexSpawnPoints;

    private void Awake()
    {
        Instance = this;

        if (indexSpawnPoints >= spawnPoints.Length)
        {
            PlayerPrefs.SetInt("pointsIndex", 0);
            indexSpawnPoints = 0;
        
        }

        //spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        indexSpawnPoints = PlayerPrefs.GetInt("pointsIndex");
        Instantiate(Player, spawnPoints[indexSpawnPoints].transform.position, Quaternion.identity);

    }

    public void lastSpawnPoint(GameObject theSpawnPoint) //poscion del jugador al recargar escena
    { 
    for (int i = 0; i < spawnPoints.Length; i++)
    { 
        
        if (spawnPoints[i] == theSpawnPoint && i > indexSpawnPoints)
            {
                PlayerPrefs.SetInt("pointsIndex", i);
            }

       
    }
    }


    private void Update() //recargar a si misma
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


    }










}

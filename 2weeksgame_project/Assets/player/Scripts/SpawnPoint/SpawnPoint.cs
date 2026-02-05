using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            //GameManager.Instance.lastSpawnPoint(gameObject);
        }
    }

}

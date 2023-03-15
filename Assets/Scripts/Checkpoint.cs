using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    static public int checkpointInstance = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats stats = FindObjectOfType<PlayerStats>();
            stats.SetSpawnPoint(transform.position);
            checkpointInstance++;
            Destroy(gameObject);
        }
    }
}

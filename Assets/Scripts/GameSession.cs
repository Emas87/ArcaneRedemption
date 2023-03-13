using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public float playerHealthPoints;
    [SerializeField] Health health;

    // Start is called before the first frame update
    void Awake()
    {
        int numberSessions = FindObjectsOfType<GameSession>().Length;
        if (numberSessions > 1)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }
        playerHealthPoints = FindObjectOfType<PlayerStats>().healthPointsCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessPlayerDeath()
    {
        GameOver();

    }
    
    void GameOver()
    {
        FindObjectOfType<ScenePersist>().Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

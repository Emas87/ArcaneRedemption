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
        if (FindObjectOfType<PlayerStats>() != null)
        {
            playerHealthPoints = FindObjectOfType<PlayerStats>().healthPointsCapacity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessPlayerDeath()
    {
        //GameOver();
        // TODO change this to go to the last checkpoint

    }

    public void GameOver()
    {
        // Player finishes the game
        //FindObjectOfType<ScenePersist>().Reset();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("GameOver");
    }

    public void LoadMenu()
    {
        // Player finishes the game
        //FindObjectOfType<ScenePersist>().Reset();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() {
        Debug.Log("Quiting Game");
        Application.Quit();
    }
}

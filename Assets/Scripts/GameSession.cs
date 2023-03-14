using System;
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
            gameObject.SetActive(false);
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

    public void StartGame()
    {
        // From menu Start button
       StartCoroutine(WaitLoad(1, "Intro"));
    }

    IEnumerator WaitLoad(float wait, string scene)
    {
        yield return new WaitForSeconds(wait);
        Destroy(gameObject);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(scene);
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
        Destroy(gameObject);
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() {
        Debug.Log("Quiting Game");
        Application.Quit();
    }

    public void OnIntroExit()
    {
        StartCoroutine(WaitLoad(0, "Level1"));
    }
}

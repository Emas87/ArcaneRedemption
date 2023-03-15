using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public float playerHealthPoints;
    public float playerEnergyPoints;
    //[SerializeField] Health health;
    

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
            playerEnergyPoints = FindObjectOfType<PlayerStats>().energyPointsCapacity;
        }
    }

    public void ProcessPlayerDeath()
    {
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2);
        PlayerMovement movement = FindObjectOfType<PlayerMovement>();
        PlayerStats stats = FindObjectOfType<PlayerStats>();
        movement._animator.SetTrigger("Respawn");
        ResetWorld();
        yield return new WaitForSeconds(0.3f);
        stats.ResetPlayer();
    }

    private void ResetWorld()
    {
        switch (Checkpoint.checkpointInstance)
        {
            case 3:
                // restore Boulder and rescued
                foreach (BoulderManager boulderManager in FindObjectsOfType<BoulderManager>())
                {
                    boulderManager.ResetBoulder();
                } 

                foreach (Rescued rescued in FindObjectsOfType<Rescued>(true))
                {
                    if(rescued.ownOrder == 5)
                    {
                        rescued.gameObject.SetActive(true);
                    }
                }
                break;
            case 6:
                // Restore Boss
                FindObjectOfType<SlimeBoss>().Reset();

                // Disable blocks
                // Enable Boss trigger
                FindObjectOfType<BossDoor>().Reset();
                break;
            case 13:
                print("toy en el ultimo checkpoint");
                FindObjectOfType<Wizard>().Reset();
                FindObjectOfType<StartFinalBattle>().Reset();
                break;
        }
                
        
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
        StartCoroutine(WaitLoad(0, "finalDeVerdad"));
    }
}

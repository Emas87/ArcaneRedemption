using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
public class LevelExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.CompareTag("Player"))
        {
            print("detecte el teleport");
            PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            player.moveToScene2();
            AudioSourceController audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSourceController>();
            audio.putLvl2Music();
            /*
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            if(nextSceneIndex != SceneManager.sceneCountInBuildSettings)
            {
                FindObjectOfType<ScenePersist>().Reset();
                SceneManager.LoadScene(nextSceneIndex);
            }
            */
        }
    }
}

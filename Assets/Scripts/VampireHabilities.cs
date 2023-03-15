using UnityEngine;

public class VampireHabilities : MonoBehaviour
{

    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void absorbLife(float incomingHealth){
        playerStats.healthPoints += incomingHealth;
        if(playerStats.healthPoints > playerStats.healthPointsCapacity){
            playerStats.healthPoints = playerStats.healthPointsCapacity;
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfHabilities : MonoBehaviour
{

    PlayerStats playerStats;
    float originalHealthPointsCapacity;
    float originalHealthPoints;


    bool werewolfActive = false;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void becomingWerewolf(){
        originalHealthPointsCapacity = playerStats.healthPointsCapacity;
        originalHealthPoints = playerStats.healthPoints;
        playerStats.healthPointsCapacity *= 2;
        playerStats.healthPoints *= 2;
        playerStats.resistence = 1;
        playerStats.strength = 2;
        werewolfActive = true;
    }

    void stopBeingWerewolf(){
        
        playerStats.healthPointsCapacity = originalHealthPointsCapacity;
        playerStats.healthPoints = originalHealthPoints;
        playerStats.resistence = 0;
        playerStats.strength = 1;
        werewolfActive = false;
    }
}

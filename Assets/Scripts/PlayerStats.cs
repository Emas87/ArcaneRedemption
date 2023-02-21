using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //script for store/managing exp, hp etc...
    // Start is called before the first frame update
    public float healthPointsCapacity = 5;
    public float healthPoints = 5;
    int experience = 0;

    public int resistence = 0;

    public int strength = 1;

    [SerializeField] public bool isDead = false;


    [SerializeField] public bool doubleJumpHability = true;
    [SerializeField] public bool dashHability = true;

    [SerializeField] public bool wolfHability = false;

    [SerializeField] public bool vampHability = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void receiveDamage(int incomingDmg){
        healthPoints -= (incomingDmg - resistence);
        if(healthPoints <= 0f){
            isDead = true;
        }
    }
}
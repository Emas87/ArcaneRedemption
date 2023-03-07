using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikes : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerStats playerStats;
    [SerializeField] float pikesDamage = 3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other) {
        // TODO fix when player stays in pykes there is no more damage
        if(other.tag == "Player"){
            PlayerStats player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
            player.receiveDamage(pikesDamage, new(0,1));
        }
    }    
}

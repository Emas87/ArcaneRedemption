using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pikes : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float pikesDamage = 3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("trigger activated");
        if(other.tag == "Player"){
            Debug.Log("Trigger with player");
            PlayerStats player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
            player.receiveDamage(pikesDamage);
        }
    }


    
}

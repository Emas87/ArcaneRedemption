using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int _speed = 3;
    [SerializeField]
    private int _life = 10;
    [SerializeField]
    private int _experience = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // IA to move closer to the player
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Vector2 direction = player.transform.position - transform.position;
        // 0.1 since position.x is never 0
        if(direction.x > 0.1)
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        } 
       if (direction.x < -0.1)
        {
            transform.Translate(-_speed * Time.deltaTime, 0, 0);
        }
    }
}

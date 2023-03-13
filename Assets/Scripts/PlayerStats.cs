using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //script for store/managing exp, hp etc...
    public float healthPointsCapacity = 100;
    public float healthPoints = 100;
    public int resistence = 0;
    public int strength = 1;
    public bool _inmunity = false;

    [SerializeField] public bool isDead = false;
    [SerializeField] public bool hasKey = false;
    [SerializeField] public bool doubleJumpHability = true;
    [SerializeField] public bool dashHability = true;
    [SerializeField] public bool wolfHability = false;
    [SerializeField] public bool vampHability = false;


    private int experience = 0;
    private Animator _animator;
    private PlayerMovement playerMovement;

    void Start()
    {
        _animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        healthPoints = FindObjectOfType<GameSession>().playerHealthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void receiveDamage(float incomingDmg, Vector2 direction){

        if (!_inmunity)
        {
            healthPoints -= (incomingDmg - resistence);
            FindObjectOfType<GameSession>().playerHealthPoints = healthPoints;
            if (healthPoints <= 0f)
            {
                isDead = true;
                Destroy(gameObject, 2f);
                _animator.SetBool("noBlood", false);
                _animator.SetBool("Death", true);
                FindObjectOfType<GameSession>().ProcessPlayerDeath();
            }
            else
            {
                _animator.SetTrigger("Hurt");
                playerMovement.Knockback(direction);
            }
        }
    }

    public void takeKey(){
        hasKey = true;
        //reproducir sonido de agarrar key?
    }

    public void removeKey(){
        hasKey = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //script for store/managing exp, hp etc...
    // Start is called before the first frame update
    public float healthPointsCapacity = 5;
    public float healthPoints = 5;
    public int resistence = 0;
    public int strength = 1;
    public bool _inminuty = false;

    [SerializeField] public bool isDead = false;
    [SerializeField] public bool hasKey = false;
    [SerializeField] public bool doubleJumpHability = true;
    [SerializeField] public bool dashHability = true;
    [SerializeField] public bool wolfHability = false;
    [SerializeField] public bool vampHability = false;


    private int experience = 0;
    private Animator _animator;
    private PlayerMovement playerMovement;
    float _inminutyTime = 0.5f;


    void Start()
    {
        _animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(hasKey);
    }
    public void receiveDamage(float incomingDmg, Vector2 direction){

        if (!_inminuty)
        {
            healthPoints -= (incomingDmg - resistence);
            if (healthPoints <= 0f)
            {
                isDead = true;
                Destroy(gameObject, 2f);
                _animator.SetBool("noBlood", false);
                _animator.SetBool("Death", true);
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

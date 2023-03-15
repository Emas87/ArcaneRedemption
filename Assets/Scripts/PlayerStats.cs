using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //script for store/managing exp, hp etc...
    public float healthPointsCapacity = 100;
    public float healthPoints = 100;
    public float energyPointsCapacity = 100;
    public float energyPoints = 100;
    public int rechargeSpeed = 4;
    public int resistence = 0;
    public int strength = 1;
    public bool _inmunity = false;
    public bool isDead = false;
    public bool hasKey = false;
    public bool doubleJumpHability = true;
    public bool dashHability = true;
    public bool wolfHability = false;
    public bool vampHability = false;

    [SerializeField] Vector2 spawnPoint= Vector2.zero;

    private Animator _animator;
    private PlayerMovement playerMovement;

    void Start()
    {
        _animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        healthPoints = FindObjectOfType<GameSession>().playerHealthPoints;
        transform.position = spawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if(energyPoints < energyPointsCapacity)
        {
            energyPoints += Time.deltaTime * rechargeSpeed;
            if(energyPoints > energyPointsCapacity)
            {
                energyPoints = energyPointsCapacity;
            }
        }
    }
    public void receiveDamage(float incomingDmg, Vector2 direction){

        if (!_inmunity && !isDead)
        {
            healthPoints -= (incomingDmg - resistence);
            FindObjectOfType<GameSession>().playerHealthPoints = healthPoints;
            if (healthPoints <= 0f)
            {
                isDead = true;
                //Destroy(gameObject, 2f);
                _animator.SetBool("noBlood", false);
                _animator.SetTrigger("Death");
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

    public void SetSpawnPoint(Vector2 newPoint)
    {
        spawnPoint = newPoint;
    }
    public Vector2 GetSpawnPoint()
    {
        return spawnPoint;
    }

    public void ResetPlayer()
    {
        healthPoints = healthPointsCapacity;
        isDead = false;
        transform.position = spawnPoint;
    }

}

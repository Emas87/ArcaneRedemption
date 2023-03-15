using System.Collections;
using UnityEngine;

public class Wizard : Enemy
{
    // Start is called before the first frame update
    Animator myAnimator;
    Vector3[] positions;
    Vector3[] specialMoveTargets;

    [SerializeField] bool battleStarted = false;
    bool onMovement = false;
    bool isTakingHit = false;
    bool isDead = false;
    bool isAttacking = false;
    bool specialMove = false;
    CapsuleCollider2D bodyCollider;
    CircleCollider2D attackCollider;

    SpriteRenderer mySpriteRenderer;

    [SerializeField] GameObject spell;
    [SerializeField] float spellForce = 10f;

    [SerializeField] AudioClip spellSound;
    [SerializeField] AudioClip hitSound;

    AudioSource myAudioSource;
    void Start()
    {
        life = 200;
        positions = new Vector3[4];
        positions[0] = new Vector3(315.27f, 31.68f, 0f);
        positions[1] = new Vector3(307.3f, 31.68f, 0f);
        positions[2] = new Vector3(298.94f, 31.68f, 0f);
        positions[3] = new Vector3(317.98f, 38.58f, 0f);
        specialMoveTargets = new Vector3[8];
        specialMoveTargets[0] = new Vector3(315.35f, 30.95f, 0f);
        specialMoveTargets[1] = new Vector3(311.52f, 30.95f, 0f);
        specialMoveTargets[2] = new Vector3(307.63f, 30.95f, 0f);
        specialMoveTargets[3] = new Vector3(303.84f, 30.95f, 0f);
        specialMoveTargets[4] = new Vector3(300.18f, 30.95f, 0f);
        specialMoveTargets[5] = new Vector3(296.13f, 30.95f, 0f);
        specialMoveTargets[6] = new Vector3(292.9f, 35.1f, 0f);
        specialMoveTargets[7] = new Vector3(289.31f, 37.2f, 0f);


        bodyCollider = GetComponent<CapsuleCollider2D>();
        attackCollider = GetComponent<CircleCollider2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public override void Update()
    {   
        if(!isDead){
            if(battleStarted && !onMovement){
                movements();
            }
            else if(battleStarted && !isAttacking){
                attack();
            }
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 playerPosition = player.transform.position;
            Vector3 myPosition = transform.position;
            if(playerPosition.x < myPosition.x){
                mySpriteRenderer.flipX = false;
            }
            else{
                mySpriteRenderer.flipX = true;
            }
        }
        else{
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            playerStats.gameOver = true;
            Destroy(this.gameObject, 0f);
        }

    }

    public void Reset(){
        battleStarted = false;
        onMovement = false;
        isAttacking = false;
        specialMove = false;
        life = 200;
        transform.position = new Vector3(307.5f, 31.84f, -0.01525223f);

    }

    public void startBattle(){
        battleStarted = true;
    }

    void movements(){
        onMovement = true;
        StartCoroutine(Teleporting());
        
    }

    IEnumerator Teleporting(){
        int randomPosition = Mathf.RoundToInt(Random.Range(0,4));
        float randomDuration = Random.Range(1.5f,4f);

        transform.position = positions[randomPosition];
        if(randomPosition == 3){
            specialMove = true;
        }

        yield return new WaitForSeconds(randomDuration);
        onMovement = false;
    }


    void attack(){
        StartCoroutine(Attack());
    }

    IEnumerator Attack(){
        isAttacking = true;
        if(specialMove){
            myAnimator.SetBool("isAttacking2", true);
            yield return new WaitForSeconds(0.7f);
            myAnimator.SetBool("isAttacking2", false);
            myAudioSource.clip = spellSound;
            myAudioSource.Play();
            for(int i = 0; i < 8; i++){
                GameObject spell_cast = Instantiate(spell, transform.position, Quaternion.identity);
                Rigidbody2D rb = spell_cast.GetComponent<Rigidbody2D>();
                Vector3 spellDirection = transform.position - specialMoveTargets[i];
                spellDirection.Normalize();
                rb.velocity = -spellDirection*spellForce;
                yield return new WaitForSeconds(0.01f);
            }
            specialMove = false;

        }
        else{
            myAnimator.SetBool("isAttacking1", true);
            yield return new WaitForSeconds(0.7f);
            myAnimator.SetBool("isAttacking1", false);
            myAudioSource.clip = spellSound;
            myAudioSource.Play();

            GameObject spell_cast = Instantiate(spell, transform.position, Quaternion.identity);
            Rigidbody2D rb = spell_cast.GetComponent<Rigidbody2D>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            Vector3 playerPosition = player.transform.position;
            Vector3 spellDirection = transform.position - playerPosition;
            spellDirection.Normalize();
            rb.velocity = -spellDirection*spellForce;
        }
        // wait for cooldown
        yield return new WaitForSeconds(1.5f);
        isAttacking = false;
    }

    public override void OnHit(int damage, Vector2 direction)
    {
        StartCoroutine(TakingHit(damage));
    }

    IEnumerator TakingHit(int damage){
        if(!isTakingHit){
            life -= damage;
            if(life > 0){
                print(life);
                isTakingHit = true;
                myAnimator.SetTrigger("takingHit");
                yield return new WaitForSeconds(0.34f);
                myAudioSource.clip = hitSound;
                myAudioSource.Play();
                isTakingHit = false;
                
            }
            else{
                myAnimator.SetTrigger("isDead");
                yield return new WaitForSeconds(0.6f);
                isDead = true;
            }
        }
    }

    IEnumerator SpecialMove(){
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(!isDead){
            //if()
        }
    }


}

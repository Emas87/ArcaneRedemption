using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{

    
    Animator myAnimator;
    CircleCollider2D attackCollider;
    CapsuleCollider2D bodyCollider;

    [SerializeField] float speed = 15f;

    [SerializeField] bool isAttacking = false;
    bool isTakingHit = false;
    bool isDead = false;

    float cooldown = 2f;

    bool onCooldown = false;
 
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        attackCollider = GetComponent<CircleCollider2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    public override void Update()
    {
        if(!isDead){
            ShouldRun();
            if (isRunning)
            {   
                Run();
            }
        }
        else{
            Destroy(this.gameObject, 1f);
        }
    }

    public void attack(){

    }

    private void OnTriggerEnter2D(Collider2D other){

        if(!isDead){
            if(attackCollider.IsTouchingLayers(LayerMask.GetMask("Player"))){
                StartCoroutine(StartChasing());
            }

            if(bodyCollider.IsTouchingLayers(LayerMask.GetMask("Player")) && !onCooldown && !isTakingHit){
                StartCoroutine(Attack());
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!cinematic && !bodyCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            isAttacking = false;
        }
    }

    IEnumerator StartChasing(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPosition = player.transform.position;
        Vector3 badPosition = transform.position;
        while(Vector3.Distance(playerPosition, badPosition) > 3.92f && !isDead){
            Vector3 target = player.transform.position;
            target.y += 1f;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }   
    }

    IEnumerator Attack(){
        isAttacking = true;
        myAnimator.SetBool("isAttacking", true);
        
        yield return new WaitForSeconds(0.7f);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerStats>().receiveDamage(10, -transform.up);
        myAnimator.SetBool("isAttacking", false);

        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;

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
                myAnimator.SetBool("isAttacking",false);
                myAnimator.SetBool("isTakingHits", true);
                yield return new WaitForSeconds(0.34f);
                myAnimator.SetBool("isTakingHits", false);
                isTakingHit = false;
            }
            else{
                myAnimator.SetBool("isDeath", true);
                isDead = true;
            }
        }
    }



}

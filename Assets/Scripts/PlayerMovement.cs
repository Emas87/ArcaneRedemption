using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{  
    Rigidbody2D myRigidBody;
    PlayerStats playerStats;
    TrailRenderer myTrailRenderer;
    CapsuleCollider2D myCapsuleCollider;
    public bool cinematic = false;

    [SerializeField] public float moveSpeed = 10;
    [SerializeField] float jumpSpeed = 25f;
    [SerializeField] float dashSpeed = 16f;
    [SerializeField] float dashTime = 0.18f;
    [SerializeField] private float attackRange = 0.9f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] Vector2 _knockback = new(10,10);

    private Animator _animator;
    float dashCoolDownTime = 0.15f;
    bool dashCoolDownActive = false;
    bool canDash = true; // put on true when landing on something
    bool isDashing = false;
    bool doubleJumpActive = false;
    bool _grounded = true;
    float _DisableTimer = 0;
    float _timeSinceAttack = 0.0f;
    int _currentAttack = 0;
    float _timeBetweenAttacks = 0.25f;
    bool _canMove = true;
    float _inmunityTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        playerStats = GetComponent<PlayerStats>();
        myTrailRenderer = GetComponent<TrailRenderer>();

        _animator.SetBool("Grounded", true);
        _animator.SetBool("Death", false);
    }

    // Update is called once per frame
    void Update()
    {
        // Updating timers
        _DisableTimer -= Time.deltaTime;
        _timeSinceAttack += Time.deltaTime;
        if (myRigidBody.velocity.x == 0)
        {
            _animator.SetInteger("AnimState", 0);
        }

        //Set AirSpeed in animator
        _animator.SetFloat("AirSpeedY", myRigidBody.velocity.y);

        //Check if character just landed on the ground
        if (!_grounded && IsGrounded() && _DisableTimer <= 0)
        {
            _grounded = true;
            _animator.SetBool("Grounded", _grounded);
        }

        //Check if character just started falling
        if (_grounded && !IsGrounded())
        {
            _grounded = false;
            _animator.SetBool("Grounded", _grounded);
        }

        if (playerStats.isDead){
            return;
        }
        if (DialogueManager.isActive || cinematic)
        {
            // Stoping player to show dialogue
            myRigidBody.velocity = Vector2.zero;
            return;
        }
        if(isDashing){
            return;
        }
        resetDash();
        if (_canMove)
        {
            Movement();
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack());
        }
    }
    bool IsGrounded()
    {
        return myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    void resetDash(){
        if(!canDash && !dashCoolDownActive){
            if(IsGrounded()){
                canDash = true;
            }
        }
    }
    void Movement(){
        if (DialogueManager.isActive)
        {
            return;
        }
        float inputX = Input.GetAxis("Horizontal");
        myRigidBody.velocity = new Vector2(inputX * moveSpeed, myRigidBody.velocity.y);
        if (myRigidBody.velocity.x < 0)
        {
            transform.localScale = new(-1 * Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            _animator.SetInteger("AnimState", 1);
        }
        else if (myRigidBody.velocity.x > 0)
        {
            transform.localScale = new(Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            _animator.SetInteger("AnimState", 1);
        }
    }

    void OnJump(InputValue val){
        if (DialogueManager.isActive)
        {
            return;
        }
        if(!playerStats.isDead && !isDashing){
            if(IsGrounded()){
                if(val.isPressed){
                    myRigidBody.velocity += new Vector2(0f, jumpSpeed);
                    if(playerStats.doubleJumpHability){
                        doubleJumpActive = true;
                    }
                    _animator.SetTrigger("Jump");
                    _grounded = false;
                    _animator.SetBool("Grounded", _grounded);
                    _DisableTimer = 0.2f;
                }
            }
            else{
                if(val.isPressed && doubleJumpActive){
                    myRigidBody.velocity += new Vector2(0f, jumpSpeed/1.5f);
                    doubleJumpActive = false;
                }
            }
        }
    }

    void OnDash(InputValue val){
        if (DialogueManager.isActive)
        {
            return;
        }
        if (!playerStats.isDead){
            if(val.isPressed && playerStats.dashHability && canDash){
                StartCoroutine(Dash());
            }
        }
    }
    IEnumerator Dash(){
        
        isDashing = true;
        canDash = false;

        float gravity = myRigidBody.gravityScale;
        myRigidBody.gravityScale = 0f;
        myRigidBody.velocity = Vector2.zero;
        float inputX = Input.GetAxis("Horizontal");
        myRigidBody.velocity = new Vector2(inputX * dashSpeed, 0f);
        
        myTrailRenderer.emitting = true;
        yield return new WaitForSeconds(dashTime);
        myTrailRenderer.emitting = false;

        myRigidBody.gravityScale = gravity;
        isDashing = false;
        
        dashCoolDownActive = true;
        yield return new WaitForSeconds(dashCoolDownTime);
        dashCoolDownActive = false;
        
    }


    IEnumerator Attack()
    {
        if (_timeSinceAttack > _timeBetweenAttacks)
        {
            // Animations
            _currentAttack++;

            // Loop back to one after third attack
            if (_currentAttack > 3)
                _currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (_timeSinceAttack > 1.0f)
                _currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            _animator.SetTrigger("Attack" + _currentAttack);

            // Reset timer
            _timeSinceAttack = 0.0f;

            // Animation 2 hits faster
            float attackAnimationTime = _currentAttack == 2 ? 0.02f : 0.03f;
            yield return new WaitForSeconds(attackAnimationTime);

            // Damages
            //Find if there is enemy in the range
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.Find("attackPoint").position, attackRange, LayerMask.GetMask("Enemies"));
            foreach (Collider2D hitEnemy in hitEnemies)
            {
                if (hitEnemy.GetType() == typeof(CapsuleCollider2D))
                {
                    Vector2 direction = transform.position - hitEnemy.transform.position;
                    hitEnemy.gameObject.GetComponent<Enemy>().OnHit(attackDamage, direction);
                }
            }
        }
    }

    public void Knockback(Vector2 direction)
    {
        myRigidBody.velocity = new(-_knockback.x * direction.x, _knockback.y);
        float scale = direction.x > 0? Mathf.Abs(transform.localScale.x): -Mathf.Abs(transform.localScale.x);
        transform.localScale = new(scale, transform.localScale.y, transform.localScale.z);
        StartCoroutine(StopControl());
        StartCoroutine(Invencibility());
    }

    IEnumerator StopControl()
    {
        _canMove = false;
        yield return new WaitForSeconds(_inmunityTime);
        _canMove = true;
    }

    IEnumerator Invencibility()
    {
        playerStats._inmunity = true;
        yield return new WaitForSeconds(_inmunityTime);
        playerStats._inmunity = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.Find("attackPoint").position, attackRange);
    }

   
}

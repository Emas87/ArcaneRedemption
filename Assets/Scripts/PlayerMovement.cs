using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{   

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    PlayerStats playerStats;
    TrailRenderer myTrailRenderer;
    CapsuleCollider2D myCapsuleCollider;

    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpSpeed = 25f;
    [SerializeField] float dashSpeed = 16f;
    [SerializeField] float dashTime = 0.18f;
    [SerializeField] private float _fireRate = 0.5f;

    private Animator _animator;
    private GameObject _projectile;
    private float nextFire = 0;
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



    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        playerStats = GetComponent<PlayerStats>();
        myTrailRenderer = GetComponent<TrailRenderer>();

        _animator.SetBool("Grounded", true);
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

        if (DialogueManager.isActive)
        {
            // Stoping player to show dialogue
            myRigidBody.velocity = Vector2.zero;
            moveInput = Vector2.zero;
            return;
        }
        if (checkIsDead()){
            return;
        }
        if(isDashing){
            return;
        }
        resetDash();
        Run();
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
    void Run(){
        if (DialogueManager.isActive)
        {
            return;
        }
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        if (playerVelocity.x < 0)
        {
            transform.localScale = new(-1 * Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            _animator.SetInteger("AnimState", 1);
        }
        else if (playerVelocity.x > 0)
        {
            transform.localScale = new(Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            _animator.SetInteger("AnimState", 1);
        }

    }

    void OnMove(InputValue val){
        if (DialogueManager.isActive)
        {
            return;
        }
        moveInput = val.Get<Vector2>();
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
        myRigidBody.velocity = new Vector2(moveInput.x*dashSpeed, 0f);
        
        myTrailRenderer.emitting = true;
        yield return new WaitForSeconds(dashTime);
        myTrailRenderer.emitting = false;

        myRigidBody.gravityScale = gravity;
        isDashing = false;
        
        dashCoolDownActive = true;
        yield return new WaitForSeconds(dashCoolDownTime);
        dashCoolDownActive = false;
        
    }

    bool checkIsDead(){
        if(playerStats.isDead){
            deadMove();
            return true;
        }
        return false;
    }
    void deadMove(){
        _animator.SetBool("noBlood", true);
        _animator.SetTrigger("Death");
    }

    void ShootProjectile()
    {
        _projectile = Resources.Load<GameObject>("Prefabs/bullet");
        nextFire = Time.time + _fireRate;
        
        Instantiate(_projectile, transform.Find("projectileSpawn").position, transform.rotation);
    }

    private void OnFire(InputValue value)
    {
        if (DialogueManager.isActive || playerStats.isDead)
        {
            return;
        }

        else if (_timeSinceAttack > _timeBetweenAttacks)
        {
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
        }
    }

}

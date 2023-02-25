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
    [SerializeField] float jumpSpeed = 29f;
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashTime = 0.2f;
    [SerializeField] private float _fireRate = 0.5f;

    private GameObject _projectile;
    private float nextFire = 0;
    float dashCoolDownTime = 0.15f;
    bool dashCoolDownActive = false;
    bool canDash = true; // put on true when landing on something
    bool isDashing = false;
    bool doubleJumpActive = false;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        playerStats = GetComponent<PlayerStats>();
        myTrailRenderer = GetComponent<TrailRenderer>();
        Debug.Log(playerStats.doubleJumpHability);
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.isActive)
        {
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
    void resetDash(){
        if(!canDash && !dashCoolDownActive){
            if(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
                canDash = true;
            }
        }
    }
    void Run(){
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        if (playerVelocity.x < 0)
            transform.localScale = new(-1 * Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (playerVelocity.x > 0)
            transform.localScale = new(Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    void OnMove(InputValue val){
        moveInput = val.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue val){
        if (DialogueManager.isActive)
        {
            return;
        }
        if (!playerStats.isDead){
            if(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
                if(val.isPressed){
                    myRigidBody.velocity += new Vector2(0f, jumpSpeed);
                    if(playerStats.doubleJumpHability){
                        doubleJumpActive = true;
                    }
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
        if(!playerStats.isDead){
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
        myRigidBody.velocity += new Vector2(moveInput.x*dashSpeed, 0f);
        
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
        //myRigidBody.velocity = new Vector2(20f, 20f);
    }

    void ShootProjectile()
    {
        _projectile = Resources.Load<GameObject>("Prefabs/bullet");
        nextFire = Time.time + _fireRate;
        
        Instantiate(_projectile, transform.Find("projectileSpawn").position, transform.rotation);
    }

    private void OnFire(InputValue value)
    {
        if (DialogueManager.isActive)
        {
            return;
        }
        if (!playerStats.isDead && Time.time > nextFire)
        {
            ShootProjectile();
        }
    }

}

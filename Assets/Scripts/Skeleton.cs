using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    BoxCollider2D frontCollider;
    EdgeCollider2D feetCollider;
    CapsuleCollider2D bodyCollider;
    CircleCollider2D attackCollider;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        frontCollider = GetComponent<BoxCollider2D>();
        feetCollider = GetComponent<EdgeCollider2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        attackCollider = GetComponent<CircleCollider2D>();
        rb.gravityScale = 2;
        jumpForce = 15;
    }

    public override void Update()
    {
        base.Update();
        ShouldRun();
        if (isRunning)
        {
            Run();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && isJumping)
        {
            isJumping = false;
        }

        bool temp1 = frontCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));

        // if front is sensing a wall and enemy is not in the air and player is not in attack range, then jump
        if (frontCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && !isJumping && !attackCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (other.gameObject.CompareTag("Platform"))
            {
                isRunning = false;
                Jump();
            }
        }        
    }


    private void ShouldRun() {
        // if user is at a certain distance start chasing it
        PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Vector2 direction = player.transform.position - transform.position;
        isRunning = direction.x <= chasingDistance;
    }

    //TODO
    public override void OnHit(int damage)
    {
        life =- damage;
    }
    public override void OnDead()
    {
    }
    public override void OnAttack()
    {
    }

}

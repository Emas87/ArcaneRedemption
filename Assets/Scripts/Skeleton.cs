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
        animator = transform.Find("Animation").GetComponent<Animator>();

        rb.gravityScale = 2;
        jumpForce = 15;
    }

    public override void Update()
    {
        base.Update();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null || player.GetComponent<PlayerStats>().isDead)
        {
            animator.SetBool("isAttacking", false);
            animator.SetBool("isRunning", false);
            return;
        }
        ShouldRun();
        if (isRunning)
        {
            Run();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!attackCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            animator.SetBool("isAttacking", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && isJumping)
        {
            isJumping = false;
        }
        animator.SetBool("isJumping", IsMoving()[1]);

        // if front is sensing a wall and enemy is not in the air and player is not in attack range, then jump
        if (frontCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && !isJumping && !attackCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (other.gameObject.CompareTag("Platform"))
            {
                Jump();
            }
        }
        if (attackCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            animator.SetBool("isAttacking", true);
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (IsDead())
        {
            return;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            OnAttack(damage, other.GetContact(0).normal);
        }
    }

}

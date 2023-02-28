using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int _speed = 3;
    [SerializeField] protected int life = 40;
    [SerializeField] protected int experience = 1;
    [SerializeField] protected bool isRunning = false;

    [SerializeField] protected float chasingDistance = 15;

    [SerializeField] protected float jumpForce = 25;
    [SerializeField] protected Vector2 deathKick = new(0f, 1f);

    protected Rigidbody2D rb;
    protected bool attacking = false;
    protected bool isJumping = false;
    protected Animator animator;

    void Start()
    {
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (IsDead())
        {
            OnDead();
            Destroy(gameObject, 1f);
            return;
        }
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", IsMoving()[1]);
    }

    public virtual void Fly() { 
    }
    public virtual void OnHit(int damage) {
        if (!IsDead())
        {
            life -= damage;
            animator.SetTrigger("onHit");
        }
    }
    public virtual void OnDead() { 
        animator.SetTrigger("isDead");
        rb.velocity = deathKick;
    }
    public virtual void OnAttack(int damage) {
        PlayerStats player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        player.receiveDamage(damage);
    }
    public virtual bool[] IsMoving()
    {
        bool[] result = new bool[2];
        result[0] = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        result[1] = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        return result;
    }
    public virtual void Jump()
    {
        if (!IsDead() && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }
    public virtual bool IsGrounded()
    {

        // Only allow 1 jump
        EdgeCollider2D collider = GetComponent<EdgeCollider2D>();
        return collider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    public virtual void Run()
    {
        if (!IsDead())
        {
            // IA to move closer to the player
            PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            Vector2 direction = player.transform.position - transform.position;
            // 0.1 since position.x is never 0
            if (direction.x > 0.1)
            {
                transform.Translate(_speed * Time.deltaTime, 0, 0);
                transform.localScale = new(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.x);
            }
            if (direction.x < -0.1)
            {
                transform.localScale = new(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.x);
                transform.Translate(-_speed * Time.deltaTime, 0, 0);
            }
        }
    }

    public virtual bool IsDead()
    {
        return life <= 0;
    }
    public virtual void ShouldRun()
    {
        // if user is at a certain distance start chasing it
        PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Vector2 direction = player.transform.position - transform.position;
        isRunning = Math.Abs(direction.x) <= chasingDistance;
    }
}

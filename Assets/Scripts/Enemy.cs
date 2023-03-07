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
    [SerializeField] protected int damage = 10;

    protected Rigidbody2D rb;
    protected bool attacking = false;
    protected bool isJumping = false;
    protected Animator animator;
    protected float attackRate = 1;
    protected float _timeSinceAttack = 0f;
    protected bool _canMove = true;
    protected bool _inminuty = false;
    protected float _inminutyTime = 0.5f;
    [SerializeField] 
    protected Vector2 _knockback = new(5, 5);


    void Start()
    {
    }

    // Update is called once per frame
    public virtual void Update()
    {
        _timeSinceAttack += Time.deltaTime;
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
    public virtual void OnHit(int damage, Vector2 direction) {
        if (!IsDead() && !_inminuty)
        {
            life -= damage;
            Debug.Log(life);
            animator.SetTrigger("onHit");
            Knockback(direction);
        }
    }
    public virtual void OnDead() { 
        animator.SetTrigger("isDead");
    }
    public virtual void OnAttack(int damage, Vector2 direction) {
        if (_timeSinceAttack > attackRate)
        {
            // Reset timer
            _timeSinceAttack = 0.0f;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                return;
            }
            player.GetComponent<PlayerStats>().receiveDamage(damage, direction);
        }
    }
    public virtual bool[] IsMoving()
    {
        bool[] result = new bool[2];
        result[0] = Mathf.Abs(rb.velocity.x) > 0.1;
        result[1] = Mathf.Abs(rb.velocity.y) > 0.1;
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
        if (!IsDead() && _canMove)
        {
            // IA to move closer to the player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) {
                return;
            }
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            Vector2 direction = playerMovement.transform.position - transform.position;
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            return;
        }
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        Vector2 direction = playerMovement.transform.position - transform.position;
        isRunning = Math.Abs(direction.x) <= chasingDistance;
    }
    public virtual void Knockback(Vector2 direction)
    {
        rb.velocity = new(-_knockback.x * direction.x, _knockback.y);
        float scale = direction.x > 0 ? -Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x);
        transform.localScale = new(scale, transform.localScale.y, transform.localScale.z);
        StartCoroutine(StopControl());
        StartCoroutine(Invencibility());
    }

    IEnumerator StopControl()
    {
        _canMove = false;
        yield return new WaitForSeconds(_inminutyTime);
        _canMove = true;
    }
    IEnumerator Invencibility()
    {
        _inminuty = true;
        yield return new WaitForSeconds(_inminutyTime);
        _inminuty = false;
    }
}

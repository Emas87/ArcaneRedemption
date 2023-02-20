using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int _speed = 3;
    [SerializeField]
    protected int life = 40;
    [SerializeField]
    protected int experience = 1;
    [SerializeField]
    protected bool isRunning = false;

    [SerializeField]
    protected float chasingDistance = 30;

    [SerializeField]
    protected float jumpForce = 25;

    protected Rigidbody2D rb;
    protected bool attacking = false;
    protected bool isJumping = false;

    void Start()
    {
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (isDead())
        {
            Destroy(gameObject);
        }
    }
    
    public virtual void Fly() { 
    }
    public virtual void OnHit(int damage) { 
    }
    public virtual void OnAttack() { 
    }
    public virtual void OnDead() { 
    }
    public virtual void Jump()
    {
        if (CanJump())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }
    public virtual bool CanJump()
    {
        // Only allow 1 jump
        EdgeCollider2D collider = GetComponent<EdgeCollider2D>();
        int temp = LayerMask.GetMask("Ground");
        return collider.IsTouchingLayers(temp);
    }
    public virtual void Run()
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

    public virtual bool isDead()
    {
        return life <= 0;
    }
}

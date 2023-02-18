using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int _speed = 3;
    [SerializeField]
    protected int life = 10;
    [SerializeField]
    protected int experience = 1;
    [SerializeField]
    protected bool isRunning = false;

    [SerializeField]
    protected float chasingDistance = 10;

    [SerializeField]
    protected float jumpForce = 25;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public virtual void Fly() { 
    }
    public virtual void OnHit() { 
    }
    public virtual void OnAttack() { 
    }
    public virtual void OnDead() { 
    }
    public virtual void Jump()
    {
    }
    public virtual bool canJump()
    {
        return true;
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


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2;
        jumpForce= 15;
    }

    public override void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump())
        {
            Jump();
        }
        ShouldRun();
        if (isRunning)
        {
            Run();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Platform") {
            isRunning = false;
            Jump();
        }

        collision.GetType();
    }

    private void ShouldRun() {
        // if user is at a certain distance start chasing it
        PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Vector2 direction = player.transform.position - transform.position;
        isRunning = direction.x <= chasingDistance;
    }

    //TODO
    public override void OnHit()
    {
    }
    public override void OnAttack()
    {
    }
    public override void OnDead()
    {
    }

}

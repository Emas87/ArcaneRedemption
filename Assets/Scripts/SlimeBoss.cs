using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : MonoBehaviour
{
    CapsuleCollider2D bodyCollider;

    public bool cinematic = false;

    [SerializeField] private int life = 200;
    [SerializeField] private int damage = 10;
    [SerializeField] private int _speed = 7;

    private Rigidbody2D rb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("cinematic", cinematic);
        if (cinematic || DialogueManager.isActive)
        {
            return;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null || player.GetComponent<PlayerStats>().isDead)
        {
            return;
        }
        if (life < 0)
        {
            animator.SetTrigger("dead");
            Destroy(gameObject, 1f);
            return;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (cinematic || DialogueManager.isActive)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.GetComponent<PlayerStats>().receiveDamage(damage, collision.GetContact(0).normal);
            }
        }
    }

    public void moveToPlayer()
    {
        Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        Vector2 target = new Vector2(playerPos.x, transform.parent.transform.position.y);
        Vector2 temp = Vector2.MoveTowards(transform.parent.position, target, _speed * Time.deltaTime);
        transform.parent.position = Vector2.MoveTowards(transform.parent.position, target, _speed * Time.deltaTime);
        Vector2 direction = playerPos - (Vector2)transform.position;
        // 0.1 since position.x is never 0
        if (direction.x > 0.1)
        {
            transform.localScale = new(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.x);
        }
        if (direction.x < -0.1)
        {
            transform.localScale = new(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.x);
        }
    }

    public void OnHit(int attackDamage)
    {
        life -= damage;        
    }
}

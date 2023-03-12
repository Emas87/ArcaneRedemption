using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : MonoBehaviour
{
    BoxCollider2D frontCollider;
    EdgeCollider2D feetCollider;
    CapsuleCollider2D bodyCollider;
    CircleCollider2D attackCollider;

    public bool cinematic = false;
    public float chasingDistance = 15;

    [SerializeField] private int life = 40;
    [SerializeField] private int damage = 10;
    [SerializeField] private int _speed = 7;

    private Rigidbody2D rb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        feetCollider = GetComponent<EdgeCollider2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponentInChildren<Animator>();
        attackCollider = GetComponent<CircleCollider2D>();
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
            animator.SetBool("running", false);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (cinematic || DialogueManager.isActive)
        {
            return;
        }

        if (attackCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            animator.SetTrigger("attacking");
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
}

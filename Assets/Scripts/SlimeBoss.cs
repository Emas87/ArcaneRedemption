using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeBoss : MonoBehaviour
{
    public bool cinematic = false;

    [SerializeField] private int life = 200;
    [SerializeField] private int lifeCapacity = 200;
    [SerializeField] private int damage = 10;
    [SerializeField] private int _speed = 7;
    [SerializeField] Slider bossLife;
    [SerializeField] GameObject blocks;
    [SerializeField] AudioClip afterDefeat;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
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
            FindObjectOfType<AudioPlayer>().PlayComplete();
            FindObjectOfType<AudioPlayer>().ChangeMusic(afterDefeat);
            Destroy(gameObject, 1f);
            blocks.SetActive(false);
            bossLife.gameObject.SetActive(false);
            return;
        }
    }

    public void StartAction()
    {
        bossLife.value = life * 100 / lifeCapacity;
        bossLife.gameObject.SetActive(true);
        transform.parent.transform.localScale = new(Mathf.Abs(transform.parent.transform.localScale.x), transform.parent.transform.localScale.y);
    }
    private void OnCollisionStay2D(Collision2D collision)
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
        Vector2 direction = playerPos - (Vector2)transform.parent.position;
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
        bossLife.value = life*100/lifeCapacity;
    }

    public void Reset()
    {
        life = lifeCapacity;
        gameObject.SetActive(false);
        bossLife.gameObject.SetActive(false);
        transform.localScale = Vector2.one;
    }
}

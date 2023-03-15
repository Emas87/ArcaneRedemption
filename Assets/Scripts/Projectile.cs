using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    [SerializeField] protected float speed = 10;
    [SerializeField] protected float lifeTime = 3;
    [SerializeField] protected int damage = 3;
    PlayerMovement player;
    float xSpeed;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * speed;
        transform.localScale = player.transform.localScale;
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody.velocity = new Vector2(xSpeed, 0f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Player"))
        {
            //Melee Hit
            Destroy(gameObject);
            return;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                Vector2 direction = transform.position - enemy.transform.position;
                enemy.OnHit(damage, direction);
            }

            SlimeBoss slimeBoss = collision.gameObject.GetComponent<SlimeBoss>();
            if (slimeBoss != null)
            {
                Vector2 direction = transform.position - slimeBoss.transform.position;
                slimeBoss.OnHit(damage);
            }

            Destroy(gameObject);
        }

    }
}

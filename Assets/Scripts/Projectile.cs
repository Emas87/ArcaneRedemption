using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    protected float speed = 10;
    protected int lifeTime = 3;
    protected int damage = 3;
    PlayerMovement player;
    float xSpeed;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * speed;
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody.velocity = new Vector2(xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Platform") || other.CompareTag("Player"))
        {
            //Melee Hit
            Destroy(gameObject);
            return;
        }
        if (other.CompareTag("Enemy"))
        {
            if (other.GetType().Name == "CapsuleCollider2D")
            {
                // nEED TO CEHCK WHAT CAN OF enemy is before getting the compeonet
                Skeleton skeleton = other.gameObject.GetComponent<Skeleton>();
                skeleton.OnHit(10);
                Destroy(gameObject);
            }
        }
    }
}

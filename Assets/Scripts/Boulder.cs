using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Boulder : MonoBehaviour
{
    Rigidbody2D RigidBody;
    [SerializeField]
    int boulderSpeed = 10;
    [SerializeField]
    int boulderDamage = 50;
    public bool running = true;

    // Start is called before the first frame update
    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            RigidBody.velocity = new Vector2(-boulderSpeed, RigidBody.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                return;
            }
            player.GetComponent<PlayerStats>().receiveDamage(boulderDamage, other.GetContact(0).normal);
        }
    }
}

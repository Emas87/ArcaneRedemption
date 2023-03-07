using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = GameObject.FindObjectOfType<PlayerMovement>();
            player.moveSpeed = 5f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = GameObject.FindObjectOfType<PlayerMovement>();
            player.moveSpeed = 10f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbChest : MonoBehaviour
{
    [SerializeField] Sprite open;

    BoxCollider2D proximity;
    ContactFilter2D contactFilter;
    List<Collider2D> colliders = new();

    bool climbGained = false;
    public void Open()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = open;
    }

    private void Start()
    {
        proximity = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if(!climbGained){
            if (Input.GetMouseButtonDown(1)) {
                proximity.OverlapCollider(contactFilter, colliders);
                foreach (var collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Player"))
                    {
                        Open();
                        Rigidbody2D rb = GameObject.FindGameObjectWithTag("DarkForestTileMap").GetComponent<Rigidbody2D>();
                        rb.sharedMaterial = null;
                        climbGained = true;
                        BoxCollider2D dialog = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<BoxCollider2D>();
                        dialog.enabled = true;
                    }
                }
            }
        }
    }
}

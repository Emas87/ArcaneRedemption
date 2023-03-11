using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] Sprite open;

    BoxCollider2D proximity;
    ContactFilter2D contactFilter;
    List<Collider2D> colliders = new();

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
        if (Input.GetMouseButtonDown(1)) {
            proximity.OverlapCollider(contactFilter, colliders);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.CompareTag("Player"))
                {
                    Open();
                }
            }
        }
    }
}

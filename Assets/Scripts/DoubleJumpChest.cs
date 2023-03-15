using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpChest : MonoBehaviour
{
    [SerializeField] Sprite open;

    BoxCollider2D proximity;
    ContactFilter2D contactFilter;
    List<Collider2D> colliders = new();

    bool doubleJumpGained = false;
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
        if(!doubleJumpGained){
            if (Input.GetMouseButtonDown(1)) {
                proximity.OverlapCollider(contactFilter, colliders);
                foreach (var collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Player"))
                    {
                        Open();
                        PlayerStats player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
                        player.doubleJumpHability = true;
                        doubleJumpGained = true;
                        BoxCollider2D dialog = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<BoxCollider2D>();
                        dialog.enabled = true;
                    }
                }
            }
        }
    }
}

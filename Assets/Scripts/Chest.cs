using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Chest : MonoBehaviour
{
    [SerializeField] Sprite open;
    [SerializeField] GameObject content;

    BoxCollider2D proximity;
    ContactFilter2D contactFilter;
    List<Collider2D> colliders = new();

    public void Open()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = open;
        GameObject contentObject = Instantiate(content, new Vector3(transform.position.x, transform.position.y + 2, 0), transform.rotation);
        Destroy(contentObject, 1f);
        if(contentObject.name.Contains("HeartPiece"))
        {
            FindObjectOfType<PlayerStats>().healthPointsCapacity += 20;
            FindObjectOfType<PlayerStats>().healthPoints += 20;
        }
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
                    FindObjectOfType<AudioPlayer>().PlayChest();
                }
            }
        }
    }
}

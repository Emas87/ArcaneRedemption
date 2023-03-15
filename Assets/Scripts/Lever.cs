using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] Sprite on;
    [SerializeField] Sprite off;
    public bool b_on = false;
    [SerializeField] MedievalDoor door;
    [SerializeField] ActivePlatform platform;
    [SerializeField] TriggerTimer timer;

    BoxCollider2D proximity;
    ContactFilter2D contactFilter;
    List<Collider2D> colliders = new();

    private void Start()
    {
        proximity = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            proximity.OverlapCollider(contactFilter, colliders);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.CompareTag("Player"))
                {
                    Switch();
                }
            }
        }
    }
    public void SwitchNoLogic()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (b_on)
        {
            b_on = false;
            spriteRenderer.sprite = off;
        }
        else
        {
            b_on = true;
            spriteRenderer.sprite = on;
        }
    }

    public void Switch()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (b_on)
        {
            b_on = false;
            spriteRenderer.sprite = off;

            if (door != null)
            {
                door.stopSpawn = true;
                door.Switch();
            }
            if (timer != null)
            {
                timer.Off(this);
            }
        } else
        {
            b_on = true;
            spriteRenderer.sprite = on;
            if (door != null)
            {
                door.stopSpawn = false;
                door.Switch();
            }
            if (timer != null)
            {
                timer.On(this);
            }
        }
        if (platform != null)
        {
            platform.Switch();
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetMouseButtonDown(1) && collision.gameObject.CompareTag("Player"))
        {
            Switch();
        }
    }
}

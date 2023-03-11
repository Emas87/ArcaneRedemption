using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public bool b_up = false;

    void Start()
    {
        if (b_up)
        {
            Up();
        }
        else
        {
            Down();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Switch()
    {
        if (b_up)
        {
            b_up = false;
            Down();
        }
        else
        {
            b_up = true;
            Up();
        }
    }

    void Up()
    {
        SpriteRenderer[] children = GetComponentsInChildren<SpriteRenderer>();
        children[0].enabled = true;
        children[1].enabled = true;
        children[2].enabled = true;
        children[3].enabled = false;
        children[4].enabled = false;
        children[5].enabled = false;
        BoxCollider2D[] colliders = GetComponentsInChildren<BoxCollider2D>();
        colliders[0].enabled = true;
        colliders[1].enabled = true;
        colliders[2].enabled = true;
    }

    void Down()
    {
        SpriteRenderer[] children = GetComponentsInChildren<SpriteRenderer>();
        children[0].enabled = false;
        children[1].enabled = false;
        children[2].enabled = false;
        children[3].enabled = true;
        children[4].enabled = true;
        children[5].enabled = true;
        BoxCollider2D[] colliders = GetComponentsInChildren<BoxCollider2D>();
        colliders[0].enabled = false;
        colliders[1].enabled = false;
        colliders[2].enabled = false;
    }
}

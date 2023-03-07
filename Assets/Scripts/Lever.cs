using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lever : MonoBehaviour
{
    [SerializeField] Sprite on;
    [SerializeField] Sprite off;
    public bool b_on = false;
    [SerializeField] GameObject triggered;

    public void Switch()
    {
        if (triggered == null)
        {
            return;
        }
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (b_on)
        {
            b_on = false;
            spriteRenderer.sprite = off;

            MedievalDoor door = triggered.GetComponent<MedievalDoor>();
            if (door != null)
            {
                door.stopSpawn = true;
                door.Switch();
            }

        } else
        {
            b_on = true;
            spriteRenderer.sprite = on;
            MedievalDoor door = triggered.GetComponent<MedievalDoor>();
            if (door != null)
            {
                door.stopSpawn = false;
                door.Switch();
            }
        }
    }
}

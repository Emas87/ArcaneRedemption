using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    Texture2D empty;
    [SerializeField]
    Texture2D filled;
    [SerializeField]
    Texture2D half;
    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        float health = FindObjectOfType<PlayerStats>().healthPoints / 20;
        float capacity = FindObjectOfType<PlayerStats>().healthPointsCapacity / 20;

        //transform.Find();
        for (int i = 0; i < 10; i++)
        {
            RawImage image = transform.Find(i.ToString()).GetComponent<RawImage>();
            image.enabled = true;
            if (health >= i + 1 ) {
                // Filled hearths
                image.texture = filled;
            } else if (i < health && health < i + 1)
            {
                // half hearth
                image.texture = half;
            }
            else if (i >= capacity)
            {
                // empty hearth
                transform.Find(i.ToString()).GetComponent<RawImage>();
                image.enabled = false;
                
            }            
            else if(i >= health)
            {
                // empty hearth
                image.texture = empty;
            }
        }
    }
}

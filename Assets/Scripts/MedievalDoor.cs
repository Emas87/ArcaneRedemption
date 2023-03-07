using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedievalDoor : MonoBehaviour
{
    [SerializeField] Sprite open;
    [SerializeField] Sprite closed;

    public bool b_open = false;
    public int enemyNumber;
    public bool stopSpawn = true;

    GameObject skeleton;
    float nextSpawn = 0;
    float spawntime = 5;

    void Start()
    {
        enemyNumber = 10;
    }
    void Update()
    {
        if (!stopSpawn && enemyNumber > 0 && nextSpawn < Time.time)
        {
            enemyNumber--;
            Spwaning();
        }
    }

    void Spwaning()
    {
        nextSpawn = Time.time + spawntime;
        if(skeleton == null)
        {
            skeleton = Resources.Load<GameObject>("Prefabs/Skeleton");
        }
        Vector3 spawnPosition = new(transform.position.x, transform.position.y - 1, transform.position.z);
        Instantiate(skeleton, spawnPosition, transform.rotation);
    }

    public void Switch()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (b_open)
        {
            b_open = false;
            spriteRenderer.sprite = closed;
        }
        else
        {
            b_open = true;
            spriteRenderer.sprite = open;
        }
    }
}

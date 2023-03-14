using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boulder"))
        {
            Boulder boulder = FindObjectOfType<Boulder>();
            boulder.running = false;
        }
    }
    public void StartBoulder()
    {
        Boulder boulder = FindObjectOfType<Boulder>();
        boulder.running = true;
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    public void ResetBoulder()
    {
        Boulder boulder = FindObjectOfType<Boulder>();
        boulder.running = false;
        boulder.transform.position = boulder.spawnPosition;
        CapsuleCollider2D collider = GetComponent<CapsuleCollider2D>();
        if(collider != null)
        {
            collider.enabled = true;
        }
    }
}

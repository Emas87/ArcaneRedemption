using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] Sprite doorOpen;

    [SerializeField] SpriteRenderer mySpriteRenderer;

    [SerializeField] Collider2D myCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myCollider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "Player"){
            Debug.Log("pegando con puerta");
            PlayerStats player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
            if(player.hasKey){
                mySpriteRenderer.sprite = doorOpen;
                player.hasKey = false;
                myCollider2D.isTrigger = true;
            }
            else{
                //Player needs the key
            }
        }
    }
}

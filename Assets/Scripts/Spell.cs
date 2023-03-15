using UnityEngine;

public class Spell : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float spellDamage = 10;
    void Start()
    {
        print("ive been cast!!!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector2 direction = transform.position - player.transform.position;

            player.GetComponent<PlayerStats>().receiveDamage(spellDamage, direction);
            Destroy(this.gameObject, 0f);
        }
        else if(collision.gameObject.CompareTag("DarkForestTileMap")){
            Destroy(this.gameObject, 0f);
        }

    }
    
}

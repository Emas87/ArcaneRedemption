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

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerStats>().receiveDamage(spellDamage, -transform.up);
            Destroy(this.gameObject, 0f);
        }
        else if(other.CompareTag("DarkForestTileMap")){
            Destroy(this.gameObject, 0f);
        }

    }
    
}

using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject myObject;
    void Start()
    {
        myObject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("trigger activated");
        if(other.tag == "Player"){
            Debug.Log("Player grab the key");
            PlayerStats player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
            player.takeKey();
            Destroy(myObject, 0f);
        }
    }
}

using UnityEngine;

public class DarkForestBackGround : MonoBehaviour
{   

    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {  
        transform.position = player.transform.position + new Vector3(0,0,10);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = player.transform.position + new Vector3(0,0,10);
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 5);
    }
}

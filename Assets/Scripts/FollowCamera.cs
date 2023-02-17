using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        transform.position = player.transform.position - new Vector3(0,0,10);
    }
}

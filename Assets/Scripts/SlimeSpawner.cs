using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    public GameObject spwan;
    // Start is called before the first frame update
    void Start()
    {
        if (spwan == null)
        {
            Debug.LogError("spawn was not defined for gameobject SlimeSpawner");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

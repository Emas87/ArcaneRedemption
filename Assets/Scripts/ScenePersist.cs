using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        int numberScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if (numberScenePersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Reset()
    {
        Destroy(gameObject);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

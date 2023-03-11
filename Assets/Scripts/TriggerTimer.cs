using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTimer : MonoBehaviour
{
    [SerializeField] List<ActivePlatform> platforms = new();
    [SerializeField] float timer = 4;
    Coroutine coroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void On(Lever lever)
    {
        coroutine = StartCoroutine(Switch(timer,lever));
    }
    public void Off(Lever lever)
    {
        foreach (var platform in platforms)
        {
            if (platform != null)
            {
                platform.Switch();
            }
        }
        StopCoroutine(coroutine);
    }

    IEnumerator Switch(float timer, Lever lever)
    {
        foreach (var platform in platforms)
        {
            if (platform != null)
            {
                platform.Switch();
            }
        }
        yield return new WaitForSeconds(timer);
        foreach (var platform in platforms)
        {
            if (platform != null)
            {
                platform.Switch();
            }
        }
        lever.SwitchNoLogic();
    }
}

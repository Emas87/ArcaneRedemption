using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroSpeech : MonoBehaviour
{
    int textIndex = 0;
    string[] conversation;
    // Start is called before the first frame update
    void Start()
    {
        conversation = new string[] { "It's getting late I should go home", "I sense something strange about you", "I think you will be useful" };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeText()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        if(textIndex < conversation.Length) {
            text.text = conversation[textIndex];        
            textIndex++;
        }
    }
}

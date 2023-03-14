using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    string[] currentMessages;
    int activeMessage = 0;
    TextMeshProUGUI message;
    Rescued currentTalking;

    public static bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        message = GetComponentInChildren<TextMeshProUGUI>();
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActive)
        {
            NextMessage();
        }
    }

    public void OpenDialogue(string[] messages, Rescued rescued)
    {
        currentTalking = rescued;
        currentMessages = messages;
        isActive = true;
        activeMessage = 0;
        DisplayMessage();
        transform.LeanScale(Vector3.one, 0.5f).setEaseInOutExpo();
    }
    void DisplayMessage()
    {
        try
        {
            if (message != null)
            {
                message.text = currentMessages[activeMessage];
                AnimateTextColor();
            }
        }
        catch (System.NullReferenceException)
        {

            throw;
        }
        
    }

    public void NextMessage() { 
        activeMessage++;
        if(activeMessage < currentMessages.Length)
        {
            if(activeMessage > 0 && currentTalking != null) {
                currentTalking.Rescue();
            }
            DisplayMessage();
            if(currentTalking != null)
            {
                currentTalking.SpecialAction(activeMessage);
            }
        } else
        {
            isActive = false;
            transform.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
            // Destroy rescued
            if (currentTalking != null)
            {
                currentTalking.DestroyRescued();
                currentTalking.gameObject.SetActive(false);
            }
        }
    }
    void AnimateTextColor()
    {
        LeanTween.value(message.gameObject, UpdateAlpha, 0, 1, 0.5f);
    }
    private void UpdateAlpha(float alpha)
    {
        // Update the alpha value of the text
        Color newColor = message.color;
        newColor.a = alpha;
        message.color = newColor;
    }
}

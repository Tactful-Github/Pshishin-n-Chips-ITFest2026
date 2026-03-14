using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NotificationManager : MonoBehaviour
{
    public Text phoneText;          // Assign your phone Text UI here
    public InputScript intscript;   // Assign the InputScript instance
    public int nbr;                 // Optional: shows current SCORE

    public List<string> messages = new List<string>()
    {
        "0",        // messages[0] will show SCORE
        "Message 2",
        "Message 3",
        "Message 4",
        "Message 5"
    };

    private void Update()
    {
        if (intscript != null)
        {
            nbr = intscript.SCORE;
            messages[0] = nbr.ToString(); // keep first message updated with SCORE
        }
    }

    public void ShowMessage(int index)
    {
        if (index >= 0 && index < messages.Count)
        {
            phoneText.text = messages[index];
        }
        else
        {
            Debug.LogWarning("Message index out of range");
        }
    }
}
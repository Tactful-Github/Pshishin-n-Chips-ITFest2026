using UnityEngine;
using UnityEngine.UI;

public class InputToNotification : MonoBehaviour
{
    public InputField inputField;                // Your legacy input field
    public NotificationManager notificationManager;

    public void SendMessageFromInput()
    {
        int index;

        if (int.TryParse(inputField.text, out index))
        {
            notificationManager.ShowMessage(index);
        }
        else
        {
            Debug.LogWarning("Input is not a valid number.");
        }
    }
}
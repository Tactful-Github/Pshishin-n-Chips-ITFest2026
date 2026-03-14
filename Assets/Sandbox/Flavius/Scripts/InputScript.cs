using UnityEngine;
using TMPro;

public class InputScript : MonoBehaviour
{
    public int SCORE = 0;
    public TMP_InputField inputField; // assign your TMP_InputField here
    private string correct_answer = "FISH";

    // Called automatically by TMP_InputField OnSubmit
    public void ValidateInput(string playerInput)
    {
        Debug.Log("Raw input received: '" + playerInput + "'");

        string normalizedInput = playerInput.Trim().ToUpper();
        Debug.Log("Normalized input: '" + normalizedInput + "'");

        if (normalizedInput == correct_answer.ToUpper())
        {
            SCORE++;
            Debug.Log("Correct! SCORE: " + SCORE);
        }
        else
        {
            Debug.Log("Wrong input. SCORE remains: " + SCORE);
        }

        // Clear input field and refocus
        if (inputField != null)
        {
            inputField.text = "";
            inputField.ActivateInputField();
        }
        else
        {
            Debug.LogWarning("TMP_InputField reference not assigned!");
        }
    }
}
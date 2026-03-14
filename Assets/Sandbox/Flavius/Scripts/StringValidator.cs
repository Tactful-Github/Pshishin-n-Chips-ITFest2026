using UnityEngine;
using UnityEngine.UI;

public class StringValidator : MonoBehaviour
{
    public InputField inputField;   // Legacy InputField where user types
    public Text scoreText;          // Legacy Text that shows the number
    public string correctAnswer;    // The valid string

    private int score = 0;

    public void ValidateInput()
    {
        string userInput = inputField.text;

        // Check if input matches the correct string
        if (userInput == correctAnswer)
        {
            score++;
            scoreText.text = score.ToString();
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Incorrect input");
        }

        // Optional: clear the input field
        inputField.text = "";
    }
}
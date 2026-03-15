using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StringValidator : MonoBehaviour
{
    public InputField inputField;   // Legacy InputField where user types
    public string correctAnswer; 
    public string nextSceneName;   // The valid string

    private int score = 0;

    public void ValidateInput()
    {
        string userInput = inputField.text;

        // Check if input matches the correct string
        if (userInput == correctAnswer)
        {
            SceneManager.LoadScene(nextSceneName);
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
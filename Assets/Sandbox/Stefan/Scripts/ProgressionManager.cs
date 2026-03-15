using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public GameObject cubicleTrigger; 
    public GameObject optionA;
    public GameObject optionB;

    void Start()
    {
        // When the scene loads, check what stage we are at
        if (GameState.stage == 1)
        {
            // This runs only if the player JUST CAME BACK from the cubicle
            cubicleTrigger.SetActive(false); // Hide old task
            optionA.SetActive(true);         // Show new task 1
            optionB.SetActive(true);         // Show new task 2
            
            Debug.Log("Detected return from cubicle. Stage 1 active.");
        }
    }
}
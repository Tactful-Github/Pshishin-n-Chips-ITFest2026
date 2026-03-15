using UnityEngine;
using System.Collections; // Required for Coroutines

public class HackedMusicPlay : MonoBehaviour
{
    [Header("References")]
    public AudioSource targetSource; 
    public AudioClip newMusicClip;  

    [Header("Activation")]
    public GameObject objectToActivate; // The new object you want to show
    public float delaySeconds = 3f;     // How long to wait

    void OnEnable()
    {
        // 1. Swap the music immediately
        if (targetSource != null && newMusicClip != null)
        {
            if (targetSource.clip != newMusicClip)
            {
                targetSource.Stop();
                targetSource.clip = newMusicClip;
                targetSource.Play();
                Debug.Log("Music swapped.");
            }
        }

        // 2. Start the timer to activate the next object
        StartCoroutine(WaitAndActivate());
    }

    IEnumerator WaitAndActivate()
    {
        // This pauses the logic for X seconds without freezing the game
        yield return new WaitForSeconds(delaySeconds);

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            GameState.stage = 1;
            Debug.Log(objectToActivate.name + " has been activated!");
        }
    }
}
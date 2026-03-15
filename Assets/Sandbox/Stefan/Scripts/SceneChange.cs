using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // Required for changing scenes

public class SceneChange : MonoBehaviour
{
    [Header("Cameras")]
    public Camera mainCamera;

    [Header("Audio")]
    public AudioSource sfxSource;   
    public AudioClip zoomWhoosh;    

    [Header("Zoom Settings")]
    public float zoomTargetSize = 3f;
    public float mainZoomDuration = 0.5f;

    [Header("Scene Management")]
    public string nextSceneName; // Type the name of your next scene here

    private bool isTransitioning = false;

    public void ActivateCubicle()
    {
        if (isTransitioning) return;
        StartCoroutine(ZoomAndSwitch());
    }

    IEnumerator ZoomAndSwitch()
    {
        isTransitioning = true;

        // --- STAGE 1: Zoom the Main Camera ---
        if (sfxSource && zoomWhoosh) sfxSource.PlayOneShot(zoomWhoosh);

        float elapsed = 0;
        float startSize = mainCamera.orthographicSize;

        while (elapsed < mainZoomDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / mainZoomDuration);
            mainCamera.orthographicSize = Mathf.Lerp(startSize, zoomTargetSize, t);
            yield return null;
        }

        // --- STAGE 2: Change Scene ---
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName); //For now test
        }
        else
        {
            Debug.LogError("Scene name is empty! Please type the name of the scene in the Inspector.");
            isTransitioning = false; // Reset so player can try again
        }
    }
}
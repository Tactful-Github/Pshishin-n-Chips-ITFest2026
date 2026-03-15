using UnityEngine;
using System.Collections;

public class CubicleLogic : MonoBehaviour
{
    [Header("Cameras")]
    public Camera mainCamera;
    public Camera levelSelectorCam;

    [Header("Audio")]
    public AudioSource sfxSource;   // The speaker
    public AudioClip zoomWhoosh;    // The "Zoom" sound effect
    public AudioClip switchClick;   // Optional: A "click" when the camera swaps

    [Header("Main Cam Ortho Zoom")]
    public float zoomTargetSize = 3f;
    public float mainZoomDuration = 0.5f;

    [Header("Selector Cam")]
    public float selectorStartOffset = 10f;
    public float selectorZoomSpeed = 3f;

    private Vector3 selectorHomePos;
    private float mainCameraDefaultSize;
    private bool isTransitioning = false;

    void Start()
    {
        if (levelSelectorCam != null)
            selectorHomePos = levelSelectorCam.transform.position;
            
        if (mainCamera != null)
            mainCameraDefaultSize = mainCamera.orthographicSize;
    }

    public void ActivateCubicle()
    {
        if (isTransitioning) return;
        StartCoroutine(FullCameraSequence());
    }

    IEnumerator FullCameraSequence()
    {
        isTransitioning = true;

        // --- STAGE 1: Main Cam Zoom ---
        if (sfxSource && zoomWhoosh) sfxSource.PlayOneShot(zoomWhoosh); // Start sound here

        float elapsed = 0;
        float startSize = mainCamera.orthographicSize;
        while (elapsed < mainZoomDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / mainZoomDuration);
            mainCamera.orthographicSize = Mathf.Lerp(startSize, zoomTargetSize, t);
            yield return null;
        }

        // --- STAGE 2: The Switch ---
        if (sfxSource && switchClick) sfxSource.PlayOneShot(switchClick); // Optional click

        mainCamera.enabled = false;
        levelSelectorCam.enabled = true;

        Vector3 selectorStartPos = selectorHomePos - (levelSelectorCam.transform.forward * selectorStartOffset);
        levelSelectorCam.transform.position = selectorStartPos;

        // --- STAGE 3: Selector Cam Zoom ---
        // Play the whoosh again or a second sound for the selector zoom
        if (sfxSource && zoomWhoosh) sfxSource.PlayOneShot(zoomWhoosh); 

        elapsed = 0;
        while (elapsed < 1.0f)
        {
            elapsed += Time.deltaTime * selectorZoomSpeed;
            float t = Mathf.SmoothStep(0, 1, elapsed);
            levelSelectorCam.transform.position = Vector3.Lerp(selectorStartPos, selectorHomePos, t);
            yield return null;
        }

        levelSelectorCam.transform.position = selectorHomePos;
        isTransitioning = false;
    }
}
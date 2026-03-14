using System.Collections;
using UnityEngine;

/// <summary>
/// Shows the assigned canvas content automatically after the mail-3 object has been reached,
/// with a configurable delay (default 3 seconds).
/// </summary>
[DefaultExecutionOrder(-100)]
public class ShowCanvasAfterMail3 : MonoBehaviour
{
    [Tooltip("The mail-3 GameObject. Canvas content will show 3 seconds after this becomes active.")]
    public GameObject mail3;

    [Tooltip("The canvas (or canvas content) to show: phone, scrollable text, button. This is hidden at start and shown after the delay once mail-3 is active.")]
    public GameObject canvasContentToShow;

    [Tooltip("Seconds to wait after mail-3 is reached before showing the canvas content.")]
    public float delayAfterMail3 = 3f;

    [Tooltip("Optional: when assigned, cursor will switch from the desktop plane to the phone plane when the canvas is shown. Assign the same object that has CursorConfineToPlane (e.g. mouseconfinement).")]
    public CursorConfineToPlane cursorConfineToPlane;

    void Awake()
    {
        HideCanvas();
    }

    void Start()
    {
        HideCanvas();
        StartCoroutine(ShowCanvasWhenMail3Reached());
    }

    void HideCanvas()
    {
        if (canvasContentToShow != null)
            canvasContentToShow.SetActive(false);
    }

    IEnumerator ShowCanvasWhenMail3Reached()
    {
        if (mail3 == null || canvasContentToShow == null)
            yield break;

        // Keep canvas hidden until we've reached mail-3 (guard against it being active in scene or enabled by something else)
        HideCanvas();

        yield return new WaitUntil(() => mail3.activeInHierarchy);
        yield return new WaitForSeconds(delayAfterMail3);

        canvasContentToShow.SetActive(true);

        // Only switch cursor to phone plane when the canvas is actually shown
        if (cursorConfineToPlane != null)
            cursorConfineToPlane.SwitchToPhonePlane();
    }
}

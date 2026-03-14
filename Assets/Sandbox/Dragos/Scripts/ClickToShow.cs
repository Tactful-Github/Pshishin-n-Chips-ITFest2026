using System.Collections;
using UnityEngine;

public class ClickToShow : MonoBehaviour
{
    [Tooltip("Object to hide on click (leave empty to hide the parent)")]
    public GameObject objectToHide;

    [Tooltip("Object to show on click")]
    public GameObject objectToShow;

    [Tooltip("Delay in seconds before showing the next object")]
    public float delay = 0f;

    void Start()
    {
        if (objectToShow != null)
            objectToShow.SetActive(false);

        var rend = GetComponent<Renderer>();
        if (rend != null)
            foreach (var mat in rend.materials)
                mat.renderQueue = 3100;
    }

    public void OnCursorClick()
    {
        if (delay > 0f)
            StartCoroutine(TransitionAfterDelay());
        else
            DoTransition();
    }

    void DoTransition()
    {
        GameObject hideTarget = objectToHide != null ? objectToHide : transform.parent?.gameObject;
        if (hideTarget != null)
            hideTarget.SetActive(false);

        if (objectToShow != null)
            objectToShow.SetActive(true);
    }

    IEnumerator TransitionAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        DoTransition();
    }
}

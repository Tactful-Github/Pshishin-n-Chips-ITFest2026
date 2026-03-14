using UnityEngine;

public class ClickToShow : MonoBehaviour
{
    [Tooltip("Object to hide on click (leave empty to hide the parent)")]
    public GameObject objectToHide;

    [Tooltip("Object to show on click")]
    public GameObject objectToShow;

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
        GameObject hideTarget = objectToHide != null ? objectToHide : transform.parent?.gameObject;
        if (hideTarget != null)
            hideTarget.SetActive(false);

        if (objectToShow != null)
            objectToShow.SetActive(true);
    }
}

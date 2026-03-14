using UnityEngine;

public class DesktopIcon : MonoBehaviour
{
    [Tooltip("Object to show/hide on double click (drag from scene)")]
    public GameObject targetObject;

    [Tooltip("Max time between clicks to count as double click")]
    public float doubleClickTime = 0.4f;

    private float _lastClickTime = -1f;

    void Start()
    {
        if (targetObject != null)
            targetObject.SetActive(false);
    }

    public void OnCursorClick()
    {
        float now = Time.unscaledTime;

        if (now - _lastClickTime <= doubleClickTime)
        {
            if (targetObject != null)
                targetObject.SetActive(true);

            _lastClickTime = -1f;
            return;
        }

        _lastClickTime = now;
    }
}

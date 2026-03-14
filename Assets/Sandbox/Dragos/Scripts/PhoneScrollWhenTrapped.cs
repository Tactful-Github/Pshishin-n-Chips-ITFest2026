using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Scrolls the assigned ScrollRect only when the cursor is trapped on the phone plane
/// (i.e. after the phone canvas is shown). No scrolling before the canvas is visible.
/// </summary>
[RequireComponent(typeof(ScrollRect))]
public class PhoneScrollWhenTrapped : MonoBehaviour
{
    [Tooltip("Cursor confine script (e.g. on mouseconfinement). Scrolling only works when this is using the phone plane.")]
    public CursorConfineToPlane cursorConfineToPlane;

    [Tooltip("Scroll sensitivity (multiplier for wheel delta).")]
    public float scrollSensitivity = 0.1f;

    ScrollRect _scrollRect;
    float _originalScrollSensitivity;

    void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
        if (_scrollRect != null)
        {
            _originalScrollSensitivity = _scrollRect.scrollSensitivity;
            _scrollRect.scrollSensitivity = 0f;
            _scrollRect.verticalNormalizedPosition = 1f;
        }
    }

    void OnDestroy()
    {
        if (_scrollRect != null)
            _scrollRect.scrollSensitivity = _originalScrollSensitivity;
    }

    void Update()
    {
        if (_scrollRect == null || cursorConfineToPlane == null)
            return;

        if (!cursorConfineToPlane.IsUsingPhonePlane)
            return;

        float wheel = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Approximately(wheel, 0f))
            return;

        float step = wheel * scrollSensitivity;
        _scrollRect.verticalNormalizedPosition = Mathf.Clamp01(_scrollRect.verticalNormalizedPosition + step);
    }
}

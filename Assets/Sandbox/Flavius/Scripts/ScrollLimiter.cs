using UnityEngine;
using UnityEngine.UI;

public class ScrollLimiter : MonoBehaviour
{
    public ScrollRect scrollRect;

    [Range(0f, 1f)] public float minScroll = 0.01f; // floor
    [Range(0f, 1f)] public float maxScroll = 0.99f; // ceiling

    void Start()
    {
        // Listen to scroll changes
        scrollRect.onValueChanged.AddListener(OnScroll);
    }

    void OnScroll(Vector2 pos)
    {
        float clamped = Mathf.Clamp(scrollRect.verticalNormalizedPosition, minScroll, maxScroll);
        if (!Mathf.Approximately(scrollRect.verticalNormalizedPosition, clamped))
            scrollRect.verticalNormalizedPosition = clamped;
    }
}
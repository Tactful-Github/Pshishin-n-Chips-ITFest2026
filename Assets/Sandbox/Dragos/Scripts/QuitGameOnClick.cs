using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Makes the GameObject clickable: on click, quits the application.
/// Works with Unity UI Button (auto-subscribes to onClick) or with the 3D cursor (OnCursorClick from CursorConfineToPlane).
/// </summary>
public class QuitGameOnClick : MonoBehaviour
{
    void Start()
    {
        var button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OnClick);
    }

    /// <summary>Called by UI Button or by CursorConfineToPlane when this object is clicked.</summary>
    public void OnCursorClick()
    {
        OnClick();
    }

    void OnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

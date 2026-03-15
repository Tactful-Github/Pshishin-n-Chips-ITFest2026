using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Makes the GameObject clickable: on click, runs the fade transition and loads a specified scene.
/// Works with Unity UI Button (auto-subscribes to onClick) or with the 3D cursor (OnCursorClick from CursorConfineToPlane).
/// </summary>
public class SceneTransitionOnClick : MonoBehaviour
{
    [Tooltip("Scene to load on click. Must match the scene name in Build Settings.")]
    public string targetSceneName = "";

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
        if (string.IsNullOrWhiteSpace(targetSceneName)) return;

        var transition = FindObjectOfType<scenetransitionscript>();
        if (transition != null)
            transition.TransitionToScene(targetSceneName);
        else
            SceneManager.LoadScene(targetSceneName);
    }
}

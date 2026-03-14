using UnityEngine;

public class CloseButton : MonoBehaviour
{
    [Tooltip("The window/plane to hide when this button is clicked. Leave empty to hide the parent object.")]
    public GameObject windowObject;

    void Start()
    {
        ForceRenderOnTop();
    }

    public void OnCursorClick()
    {
        GameObject target = windowObject != null ? windowObject : transform.parent?.gameObject;
        if (target != null)
            target.SetActive(false);
    }

    void ForceRenderOnTop()
    {
        var rend = GetComponent<Renderer>();
        if (rend == null) return;
        foreach (var mat in rend.materials)
            mat.renderQueue = 3100;
    }
}

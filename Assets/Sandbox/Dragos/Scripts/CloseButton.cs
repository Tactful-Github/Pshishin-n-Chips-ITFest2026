using UnityEngine;

public class CloseButton : MonoBehaviour
{
    [Tooltip("The window/plane to hide when this button is clicked. Leave empty to hide the parent object.")]
    public GameObject windowObject;

    [Tooltip("Expand the hitbox by this amount in local space (makes it easier to click)")]
    public float hitboxPadding = 0.5f;

    void Start()
    {
        ForceRenderOnTop();
        ExpandHitbox();
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

    void ExpandHitbox()
    {
        if (hitboxPadding <= 0f) return;

        var rend = GetComponent<Renderer>();
        if (rend == null) return;

        var meshCol = GetComponent<MeshCollider>();
        var boxCol = GetComponent<BoxCollider>();
        if (boxCol == null) boxCol = gameObject.AddComponent<BoxCollider>();

        var localBounds = rend.localBounds;
        boxCol.center = localBounds.center;
        boxCol.size = localBounds.size + Vector3.one * hitboxPadding * 2f;

        if (meshCol != null) meshCol.enabled = false;
    }
}

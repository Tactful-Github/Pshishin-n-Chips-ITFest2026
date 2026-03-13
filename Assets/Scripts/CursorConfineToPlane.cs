using UnityEngine;

public class CursorConfineToPlane : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Leave empty to use Camera.main")]
    public Camera cam;

    [Header("Cursor")]
    [Tooltip("Optional texture for the custom cursor (falls back to a white square)")]
    public Texture2D cursorTexture;
    public Vector2 customCursorSize = new Vector2(16, 16);

    private Vector3 _clampedWorldPos;
    private Vector3 _clampedScreenPos;
    private bool _hasClampedPos;
    private Bounds _meshBounds;

    void Start()
    {
        if (cam == null) cam = Camera.main;
        Cursor.visible = false;

        var mf = GetComponent<MeshFilter>();
        if (mf != null && mf.sharedMesh != null)
            _meshBounds = mf.sharedMesh.bounds;
        else
            _meshBounds = new Bounds(Vector3.zero, Vector3.one);
    }

    void Update()
    {
        if (cam == null) return;

        Plane plane = new Plane(transform.up, transform.position);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (!plane.Raycast(ray, out float enter))
        {
            _hasClampedPos = false;
            return;
        }

        Vector3 hitWorld = ray.GetPoint(enter);

        Vector3 local = transform.InverseTransformPoint(hitWorld);
        local.x = Mathf.Clamp(local.x, _meshBounds.min.x, _meshBounds.max.x);
        local.y = Mathf.Clamp(local.y, _meshBounds.min.y, _meshBounds.max.y);
        local.z = Mathf.Clamp(local.z, _meshBounds.min.z, _meshBounds.max.z);

        _clampedWorldPos = transform.TransformPoint(local);
        _clampedScreenPos = cam.WorldToScreenPoint(_clampedWorldPos);
        _hasClampedPos = true;
    }

    void OnGUI()
    {
        if (!_hasClampedPos) return;

        float x = _clampedScreenPos.x - customCursorSize.x * 0.5f;
        float y = Screen.height - _clampedScreenPos.y - customCursorSize.y * 0.5f;
        Texture tex = cursorTexture != null ? cursorTexture : Texture2D.whiteTexture;
        GUI.DrawTexture(new Rect(x, y, customCursorSize.x, customCursorSize.y), tex);
    }

    void OnDestroy()
    {
        Cursor.visible = true;
    }

    /// <summary>Clamped world position on the plane surface.</summary>
    public Vector3 GetClampedMouseWorldPosition() => _clampedWorldPos;

    /// <summary>Clamped screen position (Y flipped for GUI).</summary>
    public Vector2 GetClampedScreenPosition()
    {
        return new Vector2(_clampedScreenPos.x, Screen.height - _clampedScreenPos.y);
    }

    public bool HasClampedPosition => _hasClampedPos;
}

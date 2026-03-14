using UnityEngine;

public class CursorConfineToPlane : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Leave empty to use Camera.main")]
    public Camera cam;

    [Header("Cursor")]
    [Tooltip("Drag your cursor texture here, or it auto-loads 'cursor' from Resources")]
    public Texture2D cursorTexture;
    [Tooltip("Texture shown while the mouse button is held down")]
    public Texture2D cursorClickTexture;
    [Tooltip("Size of the cursor in world units")]
    public float cursorWorldSize = 0.5f;

    [Header("Hover Highlight")]
    [Tooltip("Layer mask for objects that can be highlighted on hover")]
    public LayerMask hoverLayerMask = ~0;
    [Tooltip("Color of the highlight quad behind hovered objects")]
    public Color highlightColor = new Color(1f, 1f, 1f, 0.3f);

    private Vector3 _clampedWorldPos;
    private bool _hasClampedPos;
    private Bounds _meshBounds;
    private GameObject _cursorObj;
    private Material _cursorMat;
    private GameObject _highlightObj;
    private Material _highlightMat;

    void Start()
    {
        if (cam == null) cam = Camera.main;

        if (cursorTexture == null)
            cursorTexture = Resources.Load<Texture2D>("cursor");

        var mf = GetComponent<MeshFilter>();
        if (mf != null && mf.sharedMesh != null)
            _meshBounds = mf.sharedMesh.bounds;
        else
            _meshBounds = new Bounds(Vector3.zero, Vector3.one);

        CreateCursorQuad();
        CreateHighlightQuad();
    }

    void CreateCursorQuad()
    {
        _cursorObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
        _cursorObj.name = "GameCursor";
        Destroy(_cursorObj.GetComponent<Collider>());

        _cursorMat = new Material(Shader.Find("Sprites/Default"));
        _cursorMat.renderQueue = 3100;
        if (cursorTexture != null)
            _cursorMat.mainTexture = cursorTexture;

        _cursorObj.GetComponent<MeshRenderer>().material = _cursorMat;
        _cursorObj.SetActive(false);
    }

    void CreateHighlightQuad()
    {
        _highlightObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
        _highlightObj.name = "HoverHighlight";
        Destroy(_highlightObj.GetComponent<Collider>());

        _highlightMat = new Material(Shader.Find("Sprites/Default"));
        _highlightMat.color = highlightColor;

        _highlightObj.GetComponent<MeshRenderer>().material = _highlightMat;
        _highlightObj.SetActive(false);
    }

    void Update()
    {
        if (cam == null || _cursorObj == null) return;

        Plane plane = new Plane(transform.up, transform.position);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (!plane.Raycast(ray, out float enter))
        {
            _cursorObj.SetActive(false);
            _highlightObj.SetActive(false);
            _hasClampedPos = false;
            return;
        }

        Vector3 hitWorld = ray.GetPoint(enter);

        Vector3 local = transform.InverseTransformPoint(hitWorld);
        local.x = Mathf.Clamp(local.x, _meshBounds.min.x, _meshBounds.max.x);
        local.y = Mathf.Clamp(local.y, _meshBounds.min.y, _meshBounds.max.y);
        local.z = Mathf.Clamp(local.z, _meshBounds.min.z, _meshBounds.max.z);

        _clampedWorldPos = transform.TransformPoint(local);

        _cursorObj.transform.position = _clampedWorldPos + transform.up * 0.01f;
        _cursorObj.transform.rotation = Quaternion.LookRotation(-transform.up, transform.forward);
        _cursorObj.transform.localScale = new Vector3(cursorWorldSize, cursorWorldSize, cursorWorldSize);
        _cursorObj.SetActive(true);
        _hasClampedPos = true;

        if (cursorClickTexture != null)
        {
            _cursorMat.mainTexture = Input.GetMouseButton(0) ? cursorClickTexture : cursorTexture;
        }

        UpdateHoverHighlight(ray);
    }

    void UpdateHoverHighlight(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, hoverLayerMask))
        {
            Renderer hoveredRenderer = hit.collider.GetComponent<Renderer>();
            if (hoveredRenderer != null && hit.collider.gameObject != gameObject)
            {
                Bounds objBounds = hoveredRenderer.bounds;

                _highlightObj.transform.position = objBounds.center
                    + transform.up * (-0.005f);

                _highlightObj.transform.rotation =
                    Quaternion.LookRotation(-transform.up, transform.forward);

                Vector3 size = objBounds.size;
                Vector3 absRight = new Vector3(
                    Mathf.Abs(transform.right.x),
                    Mathf.Abs(transform.right.y),
                    Mathf.Abs(transform.right.z));
                Vector3 absFwd = new Vector3(
                    Mathf.Abs(transform.forward.x),
                    Mathf.Abs(transform.forward.y),
                    Mathf.Abs(transform.forward.z));
                float width = Vector3.Dot(size, absRight);
                float height = Vector3.Dot(size, absFwd);
                if (width < 0.001f) width = size.x;
                if (height < 0.001f) height = size.z;

                _highlightObj.transform.localScale = new Vector3(width, height, 1f);

                _highlightMat.color = highlightColor;
                _highlightObj.SetActive(true);
                return;
            }
        }

        _highlightObj.SetActive(false);
    }

    void OnDestroy()
    {
        if (_cursorObj != null) Destroy(_cursorObj);
        if (_cursorMat != null) Destroy(_cursorMat);
        if (_highlightObj != null) Destroy(_highlightObj);
        if (_highlightMat != null) Destroy(_highlightMat);
    }

    public Vector3 GetClampedMouseWorldPosition() => _clampedWorldPos;
    public bool HasClampedPosition => _hasClampedPos;
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Fade-to-black scene transition for a top-down 2D-in-3D setup.
/// Press ESC to transition to the scene set in the Inspector.
/// </summary>
public class scenetransitionscript : MonoBehaviour
{
    [Tooltip("Scene to load when ESC is pressed. Must match the scene name in Build Settings.")]
    public string targetSceneName = "";

    [Tooltip("How long the fade-out lasts (seconds).")]
    public float fadeOutDuration = 0.6f;

    [Tooltip("How long the fade-in lasts after loading (seconds).")]
    public float fadeInDuration = 0.6f;

    [Tooltip("Optional pause while fully black before the new scene fades in (seconds).")]
    public float holdBlackDuration = 0.2f;

    static scenetransitionscript _instance;
    float _alpha;
    bool _fading;
    Texture2D _blackTex;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        _blackTex = new Texture2D(1, 1);
        _blackTex.SetPixel(0, 0, Color.black);
        _blackTex.Apply();

        _alpha = 0f;
    }

    void Update()
    {
        if (_fading || string.IsNullOrWhiteSpace(targetSceneName)) return;
        if (Input.GetKeyDown(KeyCode.Escape))
            TransitionToScene(targetSceneName);
    }

    void OnGUI()
    {
        if (_alpha <= 0f) return;

        GUI.color = new Color(0f, 0f, 0f, _alpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _blackTex);
        GUI.color = Color.white;
    }

    /// <summary>Start a fade transition to the given scene.</summary>
    public void TransitionToScene(string sceneName)
    {
        if (!_fading)
            StartCoroutine(DoTransition(sceneName));
    }

    IEnumerator DoTransition(string sceneName)
    {
        _fading = true;

        // Fade out
        float t = 0f;
        while (t < fadeOutDuration)
        {
            t += Time.unscaledDeltaTime;
            _alpha = Mathf.Clamp01(t / fadeOutDuration);
            yield return null;
        }
        _alpha = 1f;

        // Hold black
        if (holdBlackDuration > 0f)
            yield return new WaitForSecondsRealtime(holdBlackDuration);

        // Load scene
        SceneManager.LoadScene(sceneName);

        // Wait a frame for the new scene to initialise
        yield return null;

        // Fade in
        t = 0f;
        while (t < fadeInDuration)
        {
            t += Time.unscaledDeltaTime;
            _alpha = 1f - Mathf.Clamp01(t / fadeInDuration);
            yield return null;
        }
        _alpha = 0f;

        _fading = false;
    }

    void OnDestroy()
    {
        if (_blackTex != null)
            Destroy(_blackTex);
    }
}

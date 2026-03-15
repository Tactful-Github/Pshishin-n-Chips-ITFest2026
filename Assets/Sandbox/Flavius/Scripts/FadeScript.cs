using UnityEngine;
using System.Collections;

public class FadeText : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    void Start()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float duration = 2f;
        float time = 0;

        while (time < duration)
        {
            canvasGroup.alpha = 1 - (time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
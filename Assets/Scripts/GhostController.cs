using UnityEngine;
using System.Collections;

public class GhostController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on Ghost: " + gameObject.name);
        }
    }

    public void FadeAndDestroy()
    {
        Debug.Log("Starting fade-out for ghost: " + gameObject.name);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float fadeDuration = 0.5f; // Fade out duration in seconds
        float startAlpha = spriteRenderer.color.a;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(startAlpha, 0, normalizedTime);
            spriteRenderer.color = color;
            yield return null;
        }

        Debug.Log("Destroying ghost: " + gameObject.name);
        Destroy(gameObject);
    }
}

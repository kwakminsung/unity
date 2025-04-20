using System.Collections;
using UnityEngine;

public class FadeEffect1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private float minFadeTime = 1f;
    [SerializeField]
    private float maxFadeTime = 4f;
    private float fadeTime;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fadeTime = Random.Range(minFadeTime, maxFadeTime);

        StartCoroutine(nameof(FadeLoop));
    }

    private IEnumerator FadeLoop()
    {
        while (true)
        {
            yield return StartCoroutine(OnFade(1, 0));
            yield return StartCoroutine(OnFade(0, 1));
        }
    }

    private IEnumerator OnFade(float start, float end)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime / fadeTime;

            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(start, end, percent);
            spriteRenderer.color = color;

            yield return null;
        }
    }
}

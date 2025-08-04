using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance;

    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeOutDuration = 0.3f; // Fast fade to black
    [SerializeField] private float fadeInDuration = 1.5f;  // Slower fade back

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public IEnumerator Fade(float from, float to, float duration)
    {
        float time = 0f;
        Color color = fadeImage.color;

        while (time < duration)
        {
            float t = time / duration;
            color.a = Mathf.Lerp(from, to, t);
            fadeImage.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        color.a = to;
        fadeImage.color = color;
    }

    public IEnumerator FadeTransition(System.Action onFadeMiddle)
    {
        // Fade to black fast
        yield return Fade(0f, 1f, fadeOutDuration);

        // Do teleport/sound while screen is black
        onFadeMiddle?.Invoke();
        yield return new WaitForSeconds(0.2f);

        // Fade back in slowly
        yield return Fade(1f, 0f, fadeInDuration);
    }

    public void FadeIn() => StartCoroutine(Fade(1f, 0f, fadeInDuration));
    public void FadeOut() => StartCoroutine(Fade(0f, 1f, fadeOutDuration));
}

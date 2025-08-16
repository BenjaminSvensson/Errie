using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance;

    [Header("Fade Settings")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeOutDuration = 0.3f;
    [SerializeField] private float fadeInDuration = 1.5f;
    [SerializeField] private float fadeInFastThreshold = 0.7f;
    [SerializeField] private float fadeInFastMultiplier = 2f;

    public bool IsFading { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public IEnumerator FadeTransition(System.Action onFadeMiddle)
    {
        if (IsFading) yield break;
        IsFading = true;

        SetPlayerMovement(false);

        // Fades to black and lock player movement
        yield return Fade(0f, 1f, fadeOutDuration);

        onFadeMiddle?.Invoke();
        yield return new WaitForSeconds(0.2f);

        // Fades back and unlock player movement
        yield return FadeBackSmart();

        IsFading = false;
    }

    private IEnumerator Fade(float from, float to, float duration)
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

    private IEnumerator FadeBackSmart()
    {
        float time = 0f;
        float duration = fadeInDuration;
        bool movementUnlocked = false;
        Color color = fadeImage.color;

        while (time < duration)
        {
            float t = time / duration;

            // Let the player move once screen is ~30% visible
            if (!movementUnlocked && t > 0.5f)
            {
                SetPlayerMovement(true);
                movementUnlocked = true;
            }

            // Accelerate fade after threshold
            if (t > fadeInFastThreshold)
                time += Time.deltaTime * fadeInFastMultiplier;
            else
                time += Time.deltaTime;

            color.a = Mathf.Lerp(1f, 0f, t);
            fadeImage.color = color;
            yield return null;
        }
        color.a = 0f;
        fadeImage.color = color;
    }

    private void SetPlayerMovement(bool canMove)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerMovement moveScript = player.GetComponent<PlayerMovement>();
            if (moveScript != null)
                moveScript.SetCanMove(canMove);
        }
    }

    public void FadeIn() => StartCoroutine(Fade(1f, 0f, fadeInDuration));
    public void FadeOut() => StartCoroutine(Fade(0f, 1f, fadeOutDuration));
    
}

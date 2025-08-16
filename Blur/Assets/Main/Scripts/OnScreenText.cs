using UnityEngine;
using TMPro;
using System.Collections;

public class ScreenDebug : MonoBehaviour
{
    public static ScreenDebug Instance;

    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI debugText;

    [Header("Typing Settings")]
    [SerializeField] private float typeDelay = 0.03f;
    [SerializeField] private float visibleDuration = 2f;
    [SerializeField] private float fadeOutTime = 0.5f;

    private Coroutine currentRoutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

            SetAlpha(0f);
    }

    public void ShowMessage(string message)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(TypeText(message));
    }

    private IEnumerator TypeText(string message)
    {
        debugText.text = "";
        SetAlpha(1f);

        foreach (char c in message)
        {
            debugText.text += c;
            yield return new WaitForSeconds(typeDelay);
        }

        yield return new WaitForSeconds(visibleDuration);

        float t = 0f;
        while (t < fadeOutTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, t / fadeOutTime);
            SetAlpha(alpha);
            t += Time.deltaTime;
            yield return null;
        }

        SetAlpha(0f);
        debugText.text = "";
    }

    private void SetAlpha(float alpha)
    {
        Color c = debugText.color;
        c.a = alpha;
        debugText.color = c;
    }
}

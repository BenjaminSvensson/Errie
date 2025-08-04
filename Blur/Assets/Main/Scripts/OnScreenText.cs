using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OnScreenText : MonoBehaviour
{
    public static OnScreenText Instance;

    [SerializeField] private Text debugText; // Assign in inspector
    [SerializeField] private float typeDelay = 0.03f;
    [SerializeField] private float visibleDuration = 2f;

    private Coroutine currentRoutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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
        debugText.color = new Color(debugText.color.r, debugText.color.g, debugText.color.b, 1);

        foreach (char c in message)
        {
            debugText.text += c;
            yield return new WaitForSeconds(typeDelay);
        }

        yield return new WaitForSeconds(visibleDuration);

        // Fade out
        float fadeTime = 0.5f;
        float t = 0f;
        Color original = debugText.color;
        while (t < fadeTime)
        {
            float a = Mathf.Lerp(1f, 0f, t / fadeTime);
            debugText.color = new Color(original.r, original.g, original.b, a);
            t += Time.deltaTime;
            yield return null;
        }

        debugText.text = "";
    }
}

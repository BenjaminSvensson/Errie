using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    public static AmbienceManager Instance;

    private AudioSource audioSource;

    [SerializeField] private float fadeSpeed = 1.5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    public void PlayAmbience(AudioClip clip)
    {
        if (clip == null) return;

        if (audioSource.clip == clip && audioSource.isPlaying)
            return; // already playing this one

        StopAllCoroutines();
        StartCoroutine(FadeToClip(clip));
    }

    private System.Collections.IEnumerator FadeToClip(AudioClip newClip)
    {
        // fade out current
        while (audioSource.isPlaying && audioSource.volume > 0.01f)
        {
            audioSource.volume -= Time.deltaTime * fadeSpeed;
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        // fade in
        while (audioSource.volume < 1f)
        {
            audioSource.volume += Time.deltaTime * fadeSpeed;
            yield return null;
        }
        audioSource.volume = 1f;
    }
}

using System.Collections;
using UnityEngine;
using TMPro;

public class IntroSequence : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Transform introCamPosition;
    [SerializeField] private TextMeshProUGUI introTextUI;
    [SerializeField] private string[] introLines;
    [SerializeField] private float typewriterSpeed = 0.05f;

    [Header("Audio")]
    [SerializeField] private AudioSource introAudio; // assign in inspector
    [SerializeField] private AudioClip introLoopClip;

    private bool introPlaying = true;

    void Start()
    {
        // Disable camera controller so cutscene has full control
        if (cameraController != null)
            cameraController.enabled = false;

        // Move to intro cam position
        if (introCamPosition != null)
        {
            Camera.main.transform.position = introCamPosition.position;
            Camera.main.transform.rotation = introCamPosition.rotation;
        }

        // Start looping audio
        if (introAudio != null && introLoopClip != null)
        {
            introAudio.clip = introLoopClip;
            introAudio.loop = true;
            introAudio.Play();
        }

        StartCoroutine(PlayIntroSequence());
    }

    void Update()
    {
        if (introPlaying && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            EndIntro();
        }
    }

    IEnumerator PlayIntroSequence()
    {
        foreach (string line in introLines)
        {
            yield return StartCoroutine(TypeLine(line));
            yield return new WaitForSeconds(1f);
        }

        EndIntro();
    }

    IEnumerator TypeLine(string line)
    {
        introTextUI.text = "";
        foreach (char c in line)
        {
            introTextUI.text += c;
            yield return new WaitForSeconds(typewriterSpeed);
        }
    }

    void EndIntro()
    {
        introPlaying = false;

        // Stop audio
        if (introAudio != null)
            introAudio.Stop();

        // Re-enable camera controller
        if (cameraController != null)
            cameraController.enabled = true;

        // Clear text
        if (introTextUI != null)
            introTextUI.text = "";
    }
}

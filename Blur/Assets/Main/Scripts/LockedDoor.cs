using UnityEngine;
using System.Collections;

public class LockedDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string requiredItemID;
    [SerializeField] private string messageIfLocked = "It's locked.";
    [SerializeField] private string messageIfUnlocked = "You used the key.";
    [SerializeField] private Transform destinationPoint;
    [SerializeField] private AudioClip doorSound;

    private bool isOpen = false;

    public void OnInteract(PlayerInventory inventory)
    {
        if (FadeController.Instance.IsFading)
            return;

        if (isOpen)
        {
            StartCoroutine(DoTransition());
            return;
        }

        if (inventory.HasItem(requiredItemID))
        {
            ScreenDebug.Instance.ShowMessage(messageIfUnlocked);
            isOpen = true;
            StartCoroutine(DoTransition());
        }
        else
        {
            ScreenDebug.Instance.ShowMessage(messageIfLocked);
            Shake();
        }
    }

    private IEnumerator DoTransition()
    {
        yield return FadeController.Instance.FadeTransition(() =>
        {
            // Move player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc) cc.enabled = false;

            player.transform.position = destinationPoint.position;
            player.transform.rotation = destinationPoint.rotation;

            if (cc) cc.enabled = true;

            // Play sound
            if (doorSound != null)
                AudioSource.PlayClipAtPoint(doorSound, player.transform.position);
        });
    }
    
    public void Shake(float duration = 0.2f, float magnitude = 0.05f)
    {
    StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-0.4f, 0.4f) * magnitude;
            float y = Random.Range(-0.4f, 0.4f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }   
}

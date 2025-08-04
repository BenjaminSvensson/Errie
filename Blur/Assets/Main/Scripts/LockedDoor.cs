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
            Debug.Log(messageIfUnlocked);
            isOpen = true;
            StartCoroutine(DoTransition());
        }
        else
        {
            Debug.Log(messageIfLocked);
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
}

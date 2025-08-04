using UnityEngine;
using System.Collections;

public class SimpleDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform destinationPoint;
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private string messageOnUse = "You go through the door.";

    public void OnInteract(PlayerInventory inventory)
    {
        Debug.Log(messageOnUse);
        StartCoroutine(DoTransition());
    }

    private IEnumerator DoTransition()
    {
        yield return FadeController.Instance.FadeTransition(() =>
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc) cc.enabled = false;

            player.transform.position = destinationPoint.position;
            player.transform.rotation = destinationPoint.rotation;

            if (cc) cc.enabled = true;

            if (doorSound != null)
                AudioSource.PlayClipAtPoint(doorSound, player.transform.position);
        });
    }
}

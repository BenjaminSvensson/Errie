using UnityEngine;
using System.Collections;

public class SimpleDoor : MonoBehaviour, InteractibleObjects
{
    [SerializeField] private Transform destinationPoint;
    [SerializeField] private AudioClip doorSound;

    public void OnInteract(PlayerInventory inventory)
{
    if (FadeController.Instance.IsFading)
        return;
        
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

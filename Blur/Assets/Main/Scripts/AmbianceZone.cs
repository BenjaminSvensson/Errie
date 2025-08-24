using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AmbienceZone : MonoBehaviour
{
    [SerializeField] private AudioClip ambienceClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && AmbienceManager.Instance != null)
        {
            AmbienceManager.Instance.PlayAmbience(ambienceClip);
        }
    }
}

using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactRadius = 2f;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    private Transform cachedTransform;

    private void Awake()
    {
        cachedTransform = transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Collider[] hits = Physics.OverlapSphere(cachedTransform.position, interactRadius, interactLayer);

            float closestDist = Mathf.Infinity;
            InteractibleObjects closestInteractable = null;

            foreach (Collider hit in hits)
            {
                InteractibleObjects interactable = hit.GetComponent<InteractibleObjects>();
                if (interactable != null)
                {
                    float dist = Vector3.Distance(cachedTransform.position, hit.transform.position);
                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        closestInteractable = interactable;
                    }
                }
            }

            if (closestInteractable != null && PlayerInventory.Instance != null)
            {
                closestInteractable.OnInteract(PlayerInventory.Instance);
            }
            else
            {
                // Optional: Debug.Log("Nothing to interact with nearby");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}

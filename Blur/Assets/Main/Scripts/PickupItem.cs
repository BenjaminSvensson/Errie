using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    [SerializeField] private string itemID;

    public void OnInteract(PlayerInventory inventory)
    {
        inventory.PickUpItem(itemID);
        Destroy(gameObject); // remove item from scene
    }
}

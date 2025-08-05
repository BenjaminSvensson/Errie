using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    [SerializeField] private string itemID;
    [SerializeField] private string itemName;

    public void OnInteract(PlayerInventory inventory)
    {
        inventory.PickUpItem(itemID);
        ScreenDebug.Instance.ShowMessage("You picked up " + itemName);
        Destroy(gameObject);

    }
}

using UnityEngine;

public class PickupItem : MonoBehaviour, InteractibleObjects
{
    [SerializeField] private string itemID;

    public void OnInteract(PlayerInventory inventory)
    {
        GameObject prefab = ItemDatabase.Instance.GetItemPrefab(itemID);

        if (prefab == null)
        {
            Debug.LogError($"ItemDatabase: No prefab found for itemID: {itemID}");
            return;
        }

        inventory.PickUpItem(itemID, prefab, transform.position);
        ScreenDebug.Instance.ShowMessage("Picked up " + itemID);
        Destroy(gameObject);
    }
}

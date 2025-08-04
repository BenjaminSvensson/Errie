using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public string heldItemID; // could also be a ScriptableObject if you want

    private void Awake()
    {
        Instance = this;
    }

    public void PickUpItem(string itemID)
    {
        if (!string.IsNullOrEmpty(heldItemID))
        {
            DropItem();
        }

        heldItemID = itemID;
        Debug.Log("Picked up item: " + itemID);
    }

    public void DropItem()
    {
        Debug.Log("Dropped item: " + heldItemID);
        heldItemID = null;
        // You could also instantiate a dropped item prefab here if needed
    }

    public bool HasItem(string itemID)
    {
        return heldItemID == itemID;
    }

    public bool IsHoldingAnyItem()
    {
        return !string.IsNullOrEmpty(heldItemID);
    }
}

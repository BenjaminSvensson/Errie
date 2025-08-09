using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public string heldItemID;
    public GameObject heldItemPrefab;

    [SerializeField] private TextMeshProUGUI heldItemText;
    [SerializeField] private KeyCode dropKey = KeyCode.Q;
    [SerializeField] private float throwForce = 5f;
    [SerializeField] private Transform dropOrigin; // Assign an empty GameObject in front of player

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // prevent duplicates
            return;
        }

        Instance = this;
    }


    private void Update()
    {
        if (Input.GetKeyDown(dropKey))
        {
            DropItem();
        }
    }

    public void PickUpItem(string itemID, GameObject itemPrefab, Vector3 pickupPosition)
    {
        if (!string.IsNullOrEmpty(heldItemID))
            PlaceItem(pickupPosition);

        heldItemID = itemID;
        heldItemPrefab = itemPrefab;

        UpdateHeldItemUI();
    }

    public void DropItem()
    {
        if (string.IsNullOrEmpty(heldItemID) || heldItemPrefab == null)
        {
            Debug.LogWarning("No prefab assigned for held item!");
            return;
        }

        Vector3 dropPosition = dropOrigin ? dropOrigin.position : transform.position + transform.forward;
        GameObject droppedItem = Instantiate(heldItemPrefab, dropPosition, Quaternion.identity);

        Rigidbody rb = droppedItem.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce((transform.forward + Vector3.up * 0.5f).normalized * throwForce, ForceMode.Impulse);

        heldItemID = null;
        heldItemPrefab = null;

        UpdateHeldItemUI();
    }

    public void PlaceItem(Vector3 position)
    {
        if (string.IsNullOrEmpty(heldItemID) || heldItemPrefab == null)
        {
            Debug.LogWarning("No prefab assigned for held item!");
            return;
        }

        Instantiate(heldItemPrefab, position, Quaternion.identity);

        heldItemID = null;
        heldItemPrefab = null;

        UpdateHeldItemUI();
    }


    public bool HasItem(string itemID)
    {
        return heldItemID == itemID;
    }

    public bool IsHoldingAnyItem()
    {
        return !string.IsNullOrEmpty(heldItemID);
    }

    private void UpdateHeldItemUI()
    {
        if (heldItemText != null)
            heldItemText.text = $"Holding: {(string.IsNullOrEmpty(heldItemID) ? "Nothing" : heldItemID)}";
    }
    
    public void UseHeldItem()
    {
        heldItemID = null;
        heldItemPrefab = null;
        UpdateHeldItemUI();
    }


}

using UnityEngine;

public class BusEvent : MonoBehaviour
{
    [Header("Movement Points")]
    public Transform startPoint;
    public Transform stopPoint;
    public Transform endPoint;

    [Header("Movement Settings")]
    public float travelDuration = 3f;

    private bool isMoving = false;
    private Transform targetPoint;
    private float moveTimer;

    private GameObject player;

    private void Start()
    {
        if (startPoint != null)
            transform.position = startPoint.position;

        // Subscribe to GameClock updates
        GameClock.OnMinuteChanged += OnMinuteChanged;
    }

    private void OnDestroy()
    {
        GameClock.OnMinuteChanged -= OnMinuteChanged;
    }

    private void Update()
    {
        if (isMoving && targetPoint != null)
        {
            moveTimer += Time.deltaTime;
            float t = moveTimer / travelDuration;
            transform.position = Vector3.Lerp(transform.position, targetPoint.position, t);

            if (t >= 1f)
            {
                isMoving = false;
                Debug.Log("🚌 Bus reached: " + targetPoint.name);

                if (targetPoint == endPoint && player != null)
                {
                    // Unparent player when bus reaches the end
                    player.transform.SetParent(null);
                }
            }
        }
    }

    private void OnMinuteChanged(int hour, int minute)
    {
        if (hour == 0 && minute == 28)
        {
            Debug.Log("🚌 Bus arriving at stop");
            StartMovement(stopPoint);
        }
        else if (hour == 0 && minute == 30)
        {
            Debug.Log("🚌 Bus departing to end");
            StartMovement(endPoint);
        }
    }

    private void StartMovement(Transform target)
    {
        if (target == null) return;

        targetPoint = target;
        moveTimer = 0f;
        isMoving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("👤 Player boarded the bus");
            player = other.gameObject;
            player.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("👤 Player left the bus");
            player.transform.SetParent(null);
            player = null;
        }
    }
}

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [SerializeField] private float smoothSpeed = 5f;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Awake()
    {
        Instance = this;

        if (Instance == null)
        {
            Debug.LogError("No CC Set?");
            return;
        }

        targetPosition = transform.position;
        targetRotation = transform.rotation;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
    }

    public void MoveTo(Vector3 newPosition, Quaternion newRotation)
    {
        targetPosition = newPosition;
        targetRotation = newRotation;
    }
    
}

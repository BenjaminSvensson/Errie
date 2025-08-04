using UnityEngine;

public class RoomCameraZone : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController.Instance.MoveTo(
                cameraTarget.position,
                cameraTarget.rotation
            );
        }
    }
}

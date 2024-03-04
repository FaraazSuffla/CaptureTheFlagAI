using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Reference to the player's Transform
    public float distance = 5f; // Distance from the player
    public float height = 2f; // Height offset from the player
    public float rotationSpeed = 5f; // Camera rotation speed

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        // Calculate desired position and rotation
        Vector3 targetPosition = target.position - target.forward * distance + Vector3.up * height;
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);

        // Smoothly move and rotate the camera towards the desired position and rotation
        transform.position = Vector3.Lerp(transform.position, targetPosition, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}

using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 cameraOffset = new Vector3(0f, 2f, -5f);
    public float followSpeed = 5f;
    public float rotationDamping = 10f;

    private void LateUpdate()
    {
        // Calculate the target position for the camera
        Vector3 targetPosition = playerTransform.position + playerTransform.TransformDirection(cameraOffset);

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Calculate the target rotation for the camera
        Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - transform.position, playerTransform.up);

        // Smoothly rotate the camera to look at the player
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationDamping * Time.deltaTime);
    }
}

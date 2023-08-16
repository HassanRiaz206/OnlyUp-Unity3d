using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementRadius = 10f; // Radius for following and pushing
    public float moveSpeed = 5f; // Speed of sphere movement
    public float rotationSpeed = 60f; // Speed of sphere rotation
    public float pushForce = 10f; // Force to push the player away

    private Transform playerTransform; // Reference to the player's transform

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), new Vector3(playerTransform.position.x, 0f, playerTransform.position.z));

        if (distanceToPlayer <= movementRadius)
        {
            // Move the sphere towards the player's position
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            directionToPlayer.y = 0f; // Keep the movement horizontal
            transform.position += directionToPlayer * moveSpeed * Time.deltaTime;
        }

        // Rotate the sphere around its own axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Calculate the direction away from the sphere and apply push force to the player
            Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
            collision.rigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
}

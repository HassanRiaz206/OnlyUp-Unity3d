using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float movementSpeed = 3f;
    public float rotationSpeed = 3f;
    public float biteDistance = 2f;
    public float maxRadius = 10f; // Maximum radius from the starting point
    public int damagePerBite = 5; // Damage done to player per bite


    public Transform player;
    private Animator animator;
    private Vector3 startPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        startPosition = transform.position;
    }

    private void Update()
    {
        RotateAndAnimate();

        if (IsPlayerInRange())
        {
            FollowAndBite();
        }
        else
        {
            animator.SetBool("Bite", false);
        }
    }

    private bool IsPlayerInRange()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0;
        return directionToPlayer.magnitude <= maxRadius;
    }

    private void RotateAndAnimate()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        Vector3 newPosition = transform.position + transform.forward * movementSpeed * Time.deltaTime;
        float distanceToStart = Vector3.Distance(newPosition, startPosition);

        if (distanceToStart <= maxRadius)
        {
            transform.position = newPosition;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private void FollowAndBite()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0;

        if (directionToPlayer.magnitude <= biteDistance)
        {
            // Reduce player's health and trigger bite animation
           
            animator.SetBool("Bite", true);
        }
        else
        {
            animator.SetBool("Bite", false);
        }

        Quaternion newRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);

        animator.SetBool("Bite", true);
    }
}

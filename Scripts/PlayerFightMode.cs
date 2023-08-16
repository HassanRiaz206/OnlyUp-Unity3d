using UnityEngine;

public class PlayerFightMode : MonoBehaviour
{
    private Animator animator;
    public float resetDelay = 0.1f;

    public float kickForce = 5f;
    public float punchForce = 2f;
    public float superPunchForce = 10f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandlePunchInput();
        HandleKickInput();
        HandleSuperPunchInput();
    }

    private void HandlePunchInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TriggerAndResetAnimation("Punch", punchForce);
        }
    }

    private void HandleKickInput()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TriggerAndResetAnimation("Kick", kickForce);
        }
    }

    private void HandleSuperPunchInput()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TriggerAndResetAnimation("SuperPunch", superPunchForce);
        }
    }

    private void TriggerAndResetAnimation(string triggerName, float force)
    {
        animator.SetTrigger(triggerName);
        ApplyForceToEnemy(force);
        Invoke(nameof(ResetAnimationTrigger), resetDelay);
    }

    private void ApplyForceToEnemy(float force)
    {
        // Assuming you have a reference to the enemy's GameObject
        GameObject enemyGameObject = GameObject.FindGameObjectWithTag("Enemy");

        if (enemyGameObject != null)
        {
            Rigidbody enemyRigidbody = enemyGameObject.GetComponent<Rigidbody>();

            if (enemyRigidbody != null)
            {
                Vector3 forceDirection = enemyRigidbody.position - transform.position;
                forceDirection.y = 0f; // Ignore vertical direction for now
                forceDirection.Normalize();

                enemyRigidbody.AddForce(forceDirection * force, ForceMode.Impulse);
            }
        }
    }


    private void ResetAnimationTrigger()
    {
        animator.ResetTrigger("Punch");
        animator.ResetTrigger("Kick");
        animator.ResetTrigger("SuperPunch");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(2f); // Assuming 2 damage for each punch
            }
        }
    }
}

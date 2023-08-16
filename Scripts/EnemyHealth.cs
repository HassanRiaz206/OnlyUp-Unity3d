using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;

    public int scoreValue = 2; // Score added when enemy is defeated

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // Add 2 points to the player's score
        ScoringPanel scoringPanel = FindObjectOfType<ScoringPanel>();
        if (scoringPanel != null)
        {
            scoringPanel.AddScore(scoreValue);
        }

        Destroy(gameObject);
        Debug.Log("Enemy defeated!");
    }
}

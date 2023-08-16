using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the ScoringPanel script and update the score
            ScoringPanel scoringPanel = FindObjectOfType<ScoringPanel>();
            if (scoringPanel != null)
            {
                scoringPanel.CollectCollectible();
            }

            Debug.Log("Player collected a collectible!");
            Destroy(gameObject);
        }
    }
}

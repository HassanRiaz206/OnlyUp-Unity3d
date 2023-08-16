using UnityEngine;

public class CheckLevel : MonoBehaviour
{
    public GameObject levelCompletedUI;
    private bool gameStopped = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !gameStopped)
        {
            Debug.Log("Player collected");
            Destroy(gameObject);
            levelCompletedUI.SetActive(true); // Activate the UI canvas
            StopGame();
        }
    }

    private void StopGame()
    {
        Time.timeScale = 0; // Pause the game by setting time scale to 0
        gameStopped = true; // Mark the game as stopped
        // You can add other logic here to handle pausing audio, input, etc.
    }
}

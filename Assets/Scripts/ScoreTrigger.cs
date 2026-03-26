using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated) return;

        if (collision.CompareTag("Player"))
        {
            activated = true;

            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(1);
            }
        }
    }
}
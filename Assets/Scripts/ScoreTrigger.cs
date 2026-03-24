using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private bool used = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (used) return;

        if (collision.CompareTag("Player"))
        {
            ScoreManager sm = collision.GetComponent<ScoreManager>();

            if (sm != null)
            {
                sm.AddScore(1);
                Debug.Log("Score increased by 1!");
            }

            used = true;
        }
    }

    
}
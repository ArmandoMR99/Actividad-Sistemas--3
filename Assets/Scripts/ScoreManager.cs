using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int CurrentScore { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ResetRunScore()
    {
        CurrentScore = 0;
    }

    public void AddScore(int amount)
    {
        CurrentScore += amount;
        Debug.Log("Score: " + CurrentScore);
    }

    public void SubmitCurrentScore()
    {
        if (ScoreAPI.Instance != null)
        {
            ScoreAPI.Instance.SendScore(CurrentScore);
        }
    }
}
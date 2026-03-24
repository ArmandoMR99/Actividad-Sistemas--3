using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    private bool isRunning = true;

    public void AddScore(int amount)
    {
        if (!isRunning) return;

        score += amount;
        Debug.Log("Score: " + score);
    }

    public void SetScore(int value)
    {
        score = value;
    }

    public int GetScore()
    {
        return score;
    }

    public void StopScore()
    {
        isRunning = false;
    }
}
using TMPro;
using UnityEngine;

public class ScoreItemUI : MonoBehaviour
{
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI scoreText;

    public void SetData(string username, int score)
    {
        usernameText.text = username;
        scoreText.text = score.ToString();
    }
}
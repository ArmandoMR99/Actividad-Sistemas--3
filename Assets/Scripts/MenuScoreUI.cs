using UnityEngine;
using TMPro;

public class MenuScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        if (GameManager.Instance == null)
        {
            scoreText.text = "Error cargando score";
            return;
        }

        float score = GameManager.Instance.lastScore;

        if (score > 0)
            scoreText.text = "Último Score: " + score.ToString("0");
        else
            scoreText.text = "Juega para obtener un score";
    }
}
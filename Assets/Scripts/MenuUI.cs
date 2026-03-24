using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Scores()
    {
        SceneManager.LoadScene("LeaderboardScene");
    }

    public void Logout()
    {
        GameManager.Instance.token = null;
        SceneManager.LoadScene("LoginScene");
    }

    public void IrMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string token;
    public string username;
    public float lastScore;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 👈 CLAVE
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
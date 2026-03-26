using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string username;
    public string token;

    public float lastScore; // 🔥 ESTE ES EL QUE TE FALTABA

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
}
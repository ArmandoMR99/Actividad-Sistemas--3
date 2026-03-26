using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ScoreAPI : MonoBehaviour
{
    public static ScoreAPI Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SendScore(int score)
    {
        StartCoroutine(SendScoreCoroutine(score));
    }

    private IEnumerator SendScoreCoroutine(int score)
    {
        string url = "https://sid-restapi.onrender.com/api/usuarios";

        string json = "{\"username\":\"" + GameManager.Instance.username + "\",\"data\":{\"score\":" + score + "}}";

        Debug.Log("USERNAME: " + GameManager.Instance.username);
        Debug.Log("JSON ENVIADO: " + json);

        UnityWebRequest req = new UnityWebRequest(url, "PATCH");

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        req.uploadHandler = new UploadHandlerRaw(bodyRaw);
        req.downloadHandler = new DownloadHandlerBuffer();

        req.SetRequestHeader("Content-Type", "application/json");
        req.SetRequestHeader("x-token", GameManager.Instance.token);

        yield return req.SendWebRequest();

        Debug.Log("RESPUESTA API: " + req.downloadHandler.text);

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("ERROR: " + req.error);
        }
        else
        {
            Debug.Log("SCORE GUARDADO CORRECTAMENTE");
        }
    }
}
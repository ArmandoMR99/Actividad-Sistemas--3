using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

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

    IEnumerator SendScoreCoroutine(int score)
    {
        string url = "https://sid-restapi.onrender.com/api/scores"; // ⚠️ ajusta si tu endpoint es otro

        // JSON que vas a enviar
        string json = "{\"score\":" + score + "}";

        UnityWebRequest req = new UnityWebRequest(url, "POST");

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        req.uploadHandler = new UploadHandlerRaw(bodyRaw);
        req.downloadHandler = new DownloadHandlerBuffer();

        req.SetRequestHeader("Content-Type", "application/json");
        req.SetRequestHeader("x-token", GameManager.Instance.token);

        yield return req.SendWebRequest();

        Debug.Log("RESPUESTA API: " + req.downloadHandler.text);

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("ERROR AL ENVIAR SCORE: " + req.error);
        }
        else
        {
            Debug.Log("SCORE ENVIADO CORRECTAMENTE");
        }
    }

    
}
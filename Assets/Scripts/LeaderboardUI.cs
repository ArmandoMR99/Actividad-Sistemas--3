using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Usuario
{
    public string username;
    public Data data;
}

[System.Serializable]
public class Data
{
    public int score;
}

[System.Serializable]
public class UsuariosResponse
{
    public List<Usuario> usuarios;
}

public class LeaderboardUI : MonoBehaviour
{
    public GameObject scoreItemPrefab;
    public Transform content;

    void Start()
    {
        StartCoroutine(GetScores());
    }

    IEnumerator GetScores()
    {
        string url = "https://sid-restapi.onrender.com/api/usuarios";

        UnityWebRequest req = UnityWebRequest.Get(url);
        req.SetRequestHeader("x-token", GameManager.Instance.token);

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(req.error);
            yield break;
        }

        UsuariosResponse response = JsonUtility.FromJson<UsuariosResponse>(req.downloadHandler.text);

        foreach (Usuario user in response.usuarios)
        {
            GameObject item = Instantiate(scoreItemPrefab, content);

            ScoreItemUI ui = item.GetComponent<ScoreItemUI>();

            int score = 0;

            if (user.data != null)
                score = user.data.score;

            ui.SetData(user.username, score);
        }
    }
}
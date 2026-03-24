using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardUI : MonoBehaviour
{
    public Transform content;
    public GameObject scoreItemPrefab;
   
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

        if (req.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(req.downloadHandler.text);

            UsersResponse res = JsonUtility.FromJson<UsersResponse>(req.downloadHandler.text);

            int rank = 1;

            foreach (User user in res.usuarios)
            {
                GameObject obj = Instantiate(scoreItemPrefab, content);

                obj.GetComponent<ScoreItemUI>().Setup(rank, user.username, user.score);

                rank++;
            }
        }
        else
        {
            Debug.Log("Error: " + req.error);
        }
    }
}
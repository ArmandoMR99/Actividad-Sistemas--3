using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public void OnLogin()
    {
        StartCoroutine(Login());
    }

    public void OnGoToRegister()
    {
        SceneManager.LoadScene("RegisterScene");
    }

    IEnumerator Login()
    {
        string url = "https://sid-restapi.onrender.com/api/auth/login";

        string json = JsonUtility.ToJson(new UserData(usernameInput.text, passwordInput.text));

        UnityWebRequest req = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        req.uploadHandler = new UploadHandlerRaw(bodyRaw);
        req.downloadHandler = new DownloadHandlerBuffer();

        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            LoginResponse res = JsonUtility.FromJson<LoginResponse>(req.downloadHandler.text);

            GameManager.Instance.token = res.token;
            GameManager.Instance.username = usernameInput.text;

            SceneManager.LoadScene("MenuScene");
        }
        else
        {
            Debug.Log("Error login");
        }

    }

}
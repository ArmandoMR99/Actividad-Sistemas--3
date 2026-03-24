using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class RegisterUI : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public void OnRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        string url = "https://sid-restapi.onrender.com/api/usuarios";

        string json = JsonUtility.ToJson(new UserData(usernameInput.text, passwordInput.text));

        UnityWebRequest req = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        req.uploadHandler = new UploadHandlerRaw(bodyRaw);
        req.downloadHandler = new DownloadHandlerBuffer();

        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        SceneManager.LoadScene("LoginScene");

    }
}
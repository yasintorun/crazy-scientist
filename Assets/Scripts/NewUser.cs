using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class NewUser : MonoBehaviour
{
    public GameObject userPanel;
    public TMP_InputField inputUsername;
    public TextMeshProUGUI errorMessage;
    
    private void Start()
    {
        userPanel.SetActive(false);
        //PlayerPrefs.DeleteKey("userId");
    }


    public void OpenPanel()
    {
        userPanel.SetActive(true);
    }

    public void Go()
    {
        StartCoroutine(NewUSer());
    }

    public IEnumerator NewUSer()
    {
        string username = inputUsername.text.ToLower();
        Debug.Log(username);
        if(string.IsNullOrEmpty(username))
        {
            errorMessage.text = "Username cannot be empty.";
            yield return new WaitForEndOfFrame();
        }
        if(!Regex.IsMatch(username, @"^[a-zA-Z]+$")) {
            errorMessage.text = "Username is invalid.";
            yield return new WaitForEndOfFrame();
        } else
        {
            errorMessage.text = "";
            UnityWebRequest www = UnityWebRequest.Post(WebRequestManager.API_ROOT + "add?username="+username, "");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                errorMessage.text = "Sunucudan cevap alınamıyor.";
            }
            else
            {
                Result<HighScore> hs = JsonUtility.FromJson<Result<HighScore>>(www.downloadHandler.text);
                if(!hs.success)
                {
                    errorMessage.text = hs.message;
                } else
                {
                    HighScoreSystem.highScore = hs.data;
                    //UIManager.instance.StartGame();
                    PlayerPrefs.SetInt("userId", hs.data.id);
                    LevelManager.instance.StartGame();
                }
            }
        }
    }
}

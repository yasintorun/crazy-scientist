using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Result<T>
{
    public bool success;
    public string message;
    public T data;
}

/*public class DataResult
{
    public bool success;
    public string message;
    public List<HighScore> data;
}*/


public class WebRequestManager : MonoBehaviour
{
    public static readonly string API_ROOT = "https://ikariyernet.herokuapp.com/api/highScore/";
    private void Awake()
    {
        SetPlayerHighScore();
    }
    public IEnumerator PostData(string url, string data)
    {
        UnityWebRequest www = UnityWebRequest.Post(url, data);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }

        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
        }

    }

    IEnumerator GetByUserId()
    {
        if(PlayerPrefs.HasKey("userId"))
        {
            int userId = PlayerPrefs.GetInt("userId");
            UnityWebRequest www = UnityWebRequest.Get(WebRequestManager.API_ROOT + $"getById?id={userId}");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }

            else
            {
                Result<HighScore> hs = JsonUtility.FromJson<Result<HighScore>>(www.downloadHandler.text);
                if(hs.success && hs.data != null)
                {
                    HighScoreSystem.highScore = hs.data;
                }
            }
        }
    }
    public void SetPlayerHighScore()
    {
        StartCoroutine(GetByUserId());
    }
}

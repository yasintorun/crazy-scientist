using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Linq;
using System;
public class LeaderBoard : MonoBehaviour
{
    public TextMeshProUGUI highscore1;
    public TextMeshProUGUI highscore2;
    public TextMeshProUGUI highscore3;
    public TextMeshProUGUI ownScore;

    private void Awake()
    {
        StartCoroutine(GetData(WebRequestManager.API_ROOT + "getAllHighScores"));
    }

    IEnumerator GetData(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Result<HighScore[]> scores = JsonUtility.FromJson<Result<HighScore[]>>(www.downloadHandler.text);
            if(scores.success)
            {
                highscore1.text = "1: " + scores.data[0].username + " - " + scores.data[0].score;
                highscore2.text = "2: " + scores.data[1].username + " - " + scores.data[1].score;
                highscore3.text = "3: " + scores.data[2].username + " - " + scores.data[2].score;
                if (PlayerPrefs.HasKey("userId"))
                {
                    int userId = PlayerPrefs.GetInt("userId");
                    Debug.Log(userId);
                    HighScore hs = scores.data.Where(s => s.id == userId).FirstOrDefault();
                    if(hs != null)
                    {
                        int idx = scores.data.ToList().IndexOf(hs) + 1;
                        ownScore.text = idx + ": " + hs.username + " - " + hs.score;
                    }else
                    {
                        ownScore.text = "";
                    }
                }
                else
                {
                    ownScore.text = "";
                }
            }
        }
    }

}

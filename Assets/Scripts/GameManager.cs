using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public int killCount = 0;

    [HideInInspector]
    public int scoreCount = 0;

    public TextMeshProUGUI killText, waveCountText, scoreCountText, restartPanelScoreText, usernameText;
    public Animator scoreChangeAnim;
    public GameObject newBestScore;
    private WaveManager waveManager;
    private int previousScore = 0;

    [Space]
    public Texture2D cursor;

    private void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
        killCount = scoreCount = 0;
        StartCoroutine(Laugh());
        UpdateCountTexts();
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    private IEnumerator Laugh()
    {
        while (true)
        {
            yield return new WaitForSeconds(10 + Random.Range(0, 5));
            AudioManager.instance.Play("Laugh");
        }
    }

    public void EnemyIsDead(int score)
    {
        killCount++;

        scoreCount += score;
       // StartCoroutine(ChangeScoreText());
        int maxKill = waveManager.GetCurrentWaveSO().maxKill;
        if(killCount>=maxKill)
        {
            waveManager.NextWave();
        }
        UpdateCountTexts();
    }

    public void UpdateCountTexts()
    {
        killText.text = "Kill: " + killCount;
        waveCountText.text = "Next Wave: " + waveManager.GetNextWaveCounter(killCount);
        scoreCountText.text = "Score: " + scoreCount;
        restartPanelScoreText.text = "Score: " + scoreCount;
    }

    public void PlayerDead()
    {
        usernameText.text = HighScoreSystem.highScore?.username;
        if (scoreCount > HighScoreSystem.highScore.score)
        {
            newBestScore.SetActive(true);
            StartCoroutine(FindObjectOfType<WebRequestManager>().PostData(WebRequestManager.API_ROOT + $"updateScore?id={HighScoreSystem.highScore.id}&score={scoreCount}", ""));
        }
    }

    IEnumerator ChangeScoreText()
    {

        for(int i = previousScore; i<=scoreCount; i+=5)
        {
            previousScore = i;
            scoreCountText.text = "Score: " + i;
            yield return new WaitForSeconds(0.002f);
        }
    }
    
}

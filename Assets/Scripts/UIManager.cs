using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject panel;

    public void StartGame()
    {
        Time.timeScale = 1;
        if(PlayerPrefs.HasKey("userId"))
        {
            FindObjectOfType<WebRequestManager>()?.SetPlayerHighScore();

            LevelManager.instance.StartGame();
        } else
        {
            FindObjectOfType<NewUser>().OpenPanel();
        }

    }
    
    public void RestartGame()
    {
        LevelManager.instance.RestartGame();
        Time.timeScale = 1;
    }

}

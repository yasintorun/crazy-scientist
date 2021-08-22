using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public float health;
    public RectTransform healthBar;
    public GameObject deathEffect;

    public GameObject restartPanel;

    private float maxHp;
    private float hpRegeneration = 0;
    private void Start()
    {
        maxHp = health;
        hpRegeneration = FindObjectOfType<WaveManager>().GetCurrentWaveSO().playerHpRegeneration;
    }
    public bool TakeDamage(float damage)
    {
        health -= damage;
        healthBar.sizeDelta = new Vector2((health / maxHp), healthBar.sizeDelta.y);
        if(health <= 0)
        {
            Destroy(Instantiate(deathEffect, transform.position, Quaternion.identity), 0.5f);
            restartPanel.SetActive(true);
            FindObjectOfType<GameManager>().PlayerDead();
            Time.timeScale = 0;
            return true;
        }
        return false;
    }

    private float timer = 0f;
    private void LateUpdate()
    {
        timer += Time.deltaTime;
        if(timer > 1f)
        {
            health = Mathf.Clamp(health + hpRegeneration, 0, maxHp);
            timer = 0f;
        }
    }
}

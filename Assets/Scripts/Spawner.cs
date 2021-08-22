using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [HideInInspector]
    public WaveSO waveSO;
    
    [SerializeField]
    private GameObject spawnEffect;

    public int enemyCount = 0;

    private float timer = 0f;
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > waveSO.spawnWaitTime && enemyCount < waveSO.maxEnemy)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }


    public void SpawnEnemy()
    {
        SpawnEffect effect = Instantiate(spawnEffect, transform.position, Quaternion.identity)?.GetComponent<SpawnEffect>();
        effect.enemy = waveSO.GetRandomEnemies();
    }

}

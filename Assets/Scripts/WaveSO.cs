using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "wave", menuName = "MyAssets/Wave", order = 3)]
public class WaveSO : ScriptableObject
{
    public GunSO gun;
    public int maxEnemy;
    public int maxKill;
    public float spawnWaitTime;
    public float playerHpRegeneration;
    public float enemyHpRegeneration;
    public List<EnemySO> enemies;

    public EnemySO GetRandomEnemies()
    {
        int idx = Random.Range(0, enemies.Count);
        return enemies[idx];
    }

}

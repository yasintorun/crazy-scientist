using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "MyAssets/Enemy", order = 2)]
public class EnemySO : ScriptableObject
{
    public float health;
    public float damage;
    public int score;
    public bool isFlying;
    public GameObject enemyPrefab;
}

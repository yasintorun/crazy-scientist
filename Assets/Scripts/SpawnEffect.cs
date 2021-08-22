using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    public float speed;
    public GameObject spawnParticleEffect;
    [HideInInspector]
    public EnemySO enemy;
    private float randomDistance = 0;
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 randomPath = Random.Range(0, 2) == 0 ? new Vector2(1, 1) : new Vector2(-1, 1);
        rb.AddForce(randomPath * (speed + Random.Range(100, 350)));
        if (enemy.isFlying)
        {
            randomDistance = Random.Range(-1f, 2f);
        }
    }

    private void Update()
    {
        if(enemy.isFlying)
        {
            if( Mathf.Abs(randomDistance - transform.position.y) < 0.2)
            {
                EnemySpawn();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Floor"))
        {
            EnemySpawn();
        }
        
    }

    private void EnemySpawn()
    {
        GameObject effect = Instantiate(spawnParticleEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.6f);
        Instantiate(enemy.enemyPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

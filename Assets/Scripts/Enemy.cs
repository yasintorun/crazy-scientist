using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public bool isCloseHit;
    public GunSO bulletOptionSO;
    [SerializeField]
    private EnemySO enemySO;

    [SerializeField]
    public RectTransform healthBar;
    
    private float health;
    private float decreaseHealthRate;

    public void Start()
    {
        health = enemySO.health;
        decreaseHealthRate = 100 / health;
    }
    void Update()
    {
        
    }
    //200  10
    //1/2 * 10
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.sizeDelta -= Vector2.right * ((decreaseHealthRate * damage) / 100);
        if(health <= 0)
        {
            FindObjectOfType<GameManager>().EnemyIsDead(enemySO.score);
            FindObjectOfType<Spawner>().enemyCount -= 1;

            Destroy(gameObject);
        }
    }


    public void Damage()
    {
        FindObjectOfType<PlayerManager>().TakeDamage(enemySO.damage);
    }

    public void Attack(Vector2 dir, Vector2 shotPoint, Vector2 playerPos)
    {
        if(isCloseHit)
        {
            EnemyBullet bullet = Instantiate(bulletOptionSO.bullet, shotPoint, Quaternion.identity).GetComponent<EnemyBullet>();
            bullet.bulletOption = bulletOptionSO;
            bullet.Shot(dir);
        } else if(Mathf.Abs(playerPos.y - transform.position.y) < 1)
        {
            Damage();
        }
    }
}

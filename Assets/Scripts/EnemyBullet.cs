using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [HideInInspector]
    public GunSO bulletOption;
    public Rigidbody2D rb;
    private void Start()
    {
      //  rb = GetComponent<Rigidbody2D>();
    }

    public void Shot(Vector2 dir)
    {
        
        rb.AddForce(dir * bulletOption.bulletSpeed);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") ||collision.CompareTag("Bullet"))
        {
            return;
        }

        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerManager>().TakeDamage(bulletOption.damage);
        }
        ParticleSystem ps  = Instantiate(bulletOption.shotEffect, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(ps.gameObject, 1);
        Destroy(gameObject);
    }

}

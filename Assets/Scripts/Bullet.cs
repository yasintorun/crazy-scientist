using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GunSO gun;
    void Start()
    {
        Destroy(gameObject, 10);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                collision.GetComponent<Enemy>().TakeDamage(gun.damage);
                break;
            case "Player":
            case "Bullet":
                return;
            default:
                break;
        }
        GameObject effect = Instantiate(gun.shotEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        Destroy(gameObject);
    }
}

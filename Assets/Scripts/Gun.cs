using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunSO gunSO;
    public Transform bulletShotPoint;

    private  Vector2 direction;
    private float timer = 0f;
    void Start()
    {
        
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        direction = (mousePos - transform.position).normalized;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
         transform.eulerAngles = new Vector3(0, transform.rotation.y, angle);
        if(Input.GetKeyDown(KeyCode.Mouse0) && timer > gunSO.shotWaitTime)
        {
            Shot();
            timer = 0f;
        }
    }


    private void Shot()
    {
        for(int i = 0; i<gunSO.shotCount; i++)
        {
            GameObject bullet = Instantiate(gunSO.bullet, bulletShotPoint.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().gun = gunSO;
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            float maxCount = Mathf.Clamp(gunSO.shotCount, 1, 5);
            bulletRb.AddForce((direction+(new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.05f, 0.05f)) * maxCount)) * gunSO.bulletSpeed);
        }
    }
}

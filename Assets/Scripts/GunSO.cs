using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "MyAssets/Bullet", order = 1)]
public class GunSO : ScriptableObject
{
    public int shotCount;
    public float shotWaitTime;
    public float damage;
    public float bulletSpeed;
    public float lifeTime;
    public GameObject bullet;
    public GameObject shotEffect;

}

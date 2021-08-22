using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public float maxDistance;
    [Space]
    [Header(header: "Hasar")]
    public float damageWaitTime;
    public Transform shotPoint;

    [Space]
    [Header("Hareket")]
    public float speed;

    private Rigidbody2D rb;
    //private Animator anim;
    private GameObject player;

    private SpriteRenderer sp;

    private Enemy enemy;

    private bool facingRight = true;
    private float timer = .0f;
    void Start()
    {
        shotPoint = shotPoint == null ? transform: shotPoint;
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

            timer += Time.deltaTime;

            float playerDistance = transform.position.x - player.transform.position.x;


            var dir = (playerDistance < 0) ? Vector2.right : Vector2.left;

            if (dir == Vector2.right && facingRight) Flip(180);
            else if (dir == Vector2.left && !facingRight) Flip(0);


            if (Mathf.Abs(playerDistance) > maxDistance)
            {
                rb.velocity = (dir * speed * Time.deltaTime);
            }
            else
            {
                if (timer > damageWaitTime)
                {
                    Vector2 direction = (player.transform.position - shotPoint.position).normalized;
                    direction.Normalize();
                    
                    enemy.Attack(direction, shotPoint.position, player.transform.position);
                    //anim?.SetTrigger("attack");
                    timer = .0f;
                }
                rb.velocity = Vector2.zero;
            }
        
    }


    private void Flip(float angle)
    {
        sp.flipX = facingRight;
        facingRight = !facingRight;
    }
}

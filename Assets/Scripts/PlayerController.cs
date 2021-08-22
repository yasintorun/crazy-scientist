using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;

    [Space]
    public LayerMask layerMask;
    public Transform groundPoint;

    public SpriteRenderer playerSp;

    private Rigidbody2D rb;
    private float dirX;
    private bool facingRight = true;
    private int maxJump = 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if(maxJump > 0)
            {
                maxJump -= 1;
                rb.AddForce(jumpSpeed * Vector2.up);
            }
        }

        CalculateDirection();
    }

    private void FixedUpdate()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPoint.position, .2f, layerMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                maxJump = 1;
            }
        }

        rb.velocity =  new Vector2(dirX * speed * Time.deltaTime * 100, rb.velocity.y);
    }


    private void CalculateDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float selfX = transform.position.x;
        if (mousePos.x < selfX && facingRight)
        {
            Flip(180);
        }
        else if (mousePos.x > selfX && !facingRight)
        {
            Flip(0);
        }
    }

    private void Flip(float angle)
    {
        playerSp.flipX = facingRight;
        /*Vector3 scaler = transform.eulerAngles;
        scaler.y = angle;
        transform.eulerAngles = scaler;*/
        facingRight = !facingRight;
    }


}

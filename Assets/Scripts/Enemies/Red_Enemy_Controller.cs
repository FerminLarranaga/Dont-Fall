using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red_Enemy_Controller : MonoBehaviour
{

    public GameObject DiedCollider, PlayerCollider;

    public float speed, maxSpeed;
    public int direction = 1;

    private Rigidbody2D rb2d;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb2d.velocity.x > -0.01f && rb2d.velocity.x < 0.01f)
        {
            direction = -direction;
            rb2d.velocity = new Vector2(speed * direction, rb2d.velocity.y);
        }

        rb2d.AddForce(Vector2.right * speed * direction);
        float fixedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(fixedSpeed, rb2d.velocity.y);

        if (direction != 0)
        {
            transform.localScale = new Vector3(-direction, 1f, 1f);
        } else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerController Player = col.GetComponentInParent<PlayerController>();
            float yOffset = 0.4f;
            if (transform.position.y + yOffset < col.transform.position.y)
            {
                Destroy(gameObject.GetComponent<PolygonCollider2D>());
                Destroy(PlayerCollider.GetComponent<PolygonCollider2D>());
                DiedCollider.GetComponent<PolygonCollider2D>().isTrigger = false;
                anim.SetBool("Has_Died", true);
                Invoke(nameof(DestroySelf), 0.30f);
                direction = 0;
                Player.SendMessage("SetJump", true);
            } else
            {
                Player.SendMessage("EnemyKnockBack", new float[] { transform.position.x, 7 });
            }
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Plataforma"))
        {
            transform.parent = col.transform;
        }
    }

    void OnColiisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Plataforma"))
        {
            transform.parent = null;
        }
    }
}

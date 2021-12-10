using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{

    public float speed = 5f;
    public float MaxSpeed = 0.1f;

    private bool goUp, canDoubleJump = false;
    private Rigidbody2D rb2dPlayer;

    void FixedUpdate()
    {
        if (goUp)
        {
            rb2dPlayer.AddForce(Vector2.up * speed);

            float LimitedSpeed = Mathf.Clamp(rb2dPlayer.velocity.y, MaxSpeed, -MaxSpeed);
            rb2dPlayer.velocity = new Vector2(rb2dPlayer.velocity.x, LimitedSpeed);

            rb2dPlayer.gameObject.SendMessage("PlayerIsUsingRope", true);

            goUp = false;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (rb2dPlayer == null)
            {
                rb2dPlayer = col.GetComponentInParent<Rigidbody2D>();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                goUp = true;
                canDoubleJump = true;
                rb2dPlayer.gameObject.SendMessage("PlayerIsUsingRope", true);
            } else
            {
                PlayerController Player = rb2dPlayer.gameObject.GetComponent<PlayerController>();
                Player.SetDoubleJump(canDoubleJump);

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Invoke("EnableDoubleJump", 0.01f);
                }
                goUp = false;
                rb2dPlayer.gameObject.SendMessage("PlayerIsUsingRope", false);
            }
        }
    }

    void OnTriggerExit2D()
    {
        if (rb2dPlayer != null)
        {
            rb2dPlayer.gameObject.SendMessage("PlayerIsUsingRope", false);
        }
    }

    void EnableDoubleJump()
    {
        canDoubleJump = false;
    }
}

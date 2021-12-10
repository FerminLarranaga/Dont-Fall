using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpController : MonoBehaviour
{

    public float newSpeedLimit, newJumpPower;

    private SpriteRenderer spr;
    private bool isDecreasingA = true, isFalling = true, hasBeenCathed = false;
    private float targetY;
    private Vector2 velocity;
    private AudioSource audio;
    private PlayerController player;
    private int playerLives = 0;

    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        targetY = transform.position.y - 0.1f;
        audio = GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAlpha();
        HandleFlotation();
        HandlePlayerRespawn();
    }

    void HandleAlpha()
    {
        if ((spr.color.a * 255 > 100 && isDecreasingA) || hasBeenCathed)
        {
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, (((spr.color.a * 255) - 3) / 255));
        }
        else
        {
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, (((spr.color.a * 255) + 3) / 255));
            isDecreasingA = false;

            if (spr.color.a * 255 > 250)
            {
                isDecreasingA = true;
            }
        }
    }

    void HandleFlotation()
    {
        if ((Mathf.Round(transform.position.y * 100) / 100) !=
            Mathf.Round(targetY * 100) / 100)
        {
            float posY = Mathf.SmoothDamp(transform.position.y, targetY, ref velocity.y, 0.2f);

            transform.position = new Vector2(transform.position.x, posY);
        }
        else
        {
            if (isFalling)
            {
                targetY += 0.1f;
                isFalling = false;
            }
            else
            {
                targetY -= 0.1f;
                isFalling = true;
            }
        }
    }

    void HandlePlayerRespawn()
    {
        if (player != null)
        {
            if (playerLives == 0)
            {
                playerLives = player.GetLives();
            }

            if (player.GetLives() != playerLives)
            {
                playerLives = player.GetLives();
                hasBeenCathed = false;
                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasBeenCathed)
        {
            audio.Play();
            hasBeenCathed = true;
            player.maxSpeed = newSpeedLimit;
            player.jumpPower = newJumpPower;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCheckpoint : MonoBehaviour
{

    private ParticleSystem particles;
    private bool hasStarted = false, isFalling = true;
    private float targetY;

    private Vector2 velocity;

    void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        targetY = transform.position.y -0.1f;
    }

    void Update()
    {
        if ((Mathf.Round(transform.position.y * 100)/100) !=
            Mathf.Round(targetY * 100) / 100)
        {
            float posY = Mathf.SmoothDamp(transform.position.y, targetY, ref velocity.y, 0.2f);

            transform.position = new Vector2(transform.position.x, posY);
        } else
        {
            if (isFalling)
            {
                targetY += 0.1f;
                isFalling = false;
            } else
            {
                targetY -= 0.1f;
                isFalling = true;
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !hasStarted)
        {
            hasStarted = true;
            PlayerController Player = col.GetComponentInParent<PlayerController>();
            Player.SendMessage("SetRespawnPoint", new Vector2(transform.position.x, transform.position.y));
            particles.Play();

            StartCoroutine(Desvanecer(GetComponent<SpriteRenderer>()));
        }
    }

    IEnumerator Desvanecer(SpriteRenderer spr)
    {
        while (true)
        {
            float a = Mathf.Lerp(spr.color.a, 1, Time.deltaTime * 2);

            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, a);
            yield return null;

            if (!particles.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}

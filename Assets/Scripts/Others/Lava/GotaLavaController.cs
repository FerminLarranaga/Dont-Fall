using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotaLavaController : MonoBehaviour
{

    public float impulseSpeed, power = 7;
    public float minImpulseTime = 5, maxImpulseTime = 13.5f;

    private Rigidbody2D rb2d;
    private Vector3 start;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        start = transform.position;

        InvokeRepeating("ImpulsoGota", Random.Range(0, 10f), Random.Range(minImpulseTime, maxImpulseTime));
    }

    void ImpulsoGota()
    {
        float randomXPosition = Random.Range(-2f, 2f);
        transform.position = new Vector3(start.x + randomXPosition, start.y, start.z);

        rb2d.bodyType = RigidbodyType2D.Dynamic;
        rb2d.AddForce(Vector2.up * impulseSpeed, ForceMode2D.Impulse);

        Invoke("EnableKinematic", 2f);
    }

    void EnableKinematic()
    {
        rb2d.bodyType = RigidbodyType2D.Static;
        transform.position = start;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerController Player = col.GetComponentInParent<PlayerController>();
            Player.SendMessage("EnemyKnockBack", new float[] { transform.position.x, power });
        }
    }
}

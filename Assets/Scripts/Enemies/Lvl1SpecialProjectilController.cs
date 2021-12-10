using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl1SpecialProjectilController : MonoBehaviour
{

    public Transform target;
    public ParticleSystem propulsion;
    public float playerKnockBackPower, speed;

    private Vector2 InitialPos, InitialTargetPos;
    private PlayerController player;
    private int playerLives = 0;
    private bool canContinue;

    void Start()
    {
        InitialPos = transform.position;
        InitialTargetPos = target.transform.position;
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        if (player.transform.position.x > InitialTargetPos.x)
        {
            Destroy(gameObject);
        }
    }

    void Update()
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

                if (player.transform.position.x < InitialTargetPos.x)
                {
                    ReiniciarProjectil();
                }
            }
        }
    }

    public void ReiniciarProjectil()
    {
        canContinue = false;
        propulsion.Stop();
        transform.position = InitialPos;
        target.GetComponent<Lvl1SpecialRangeActiveController>().HasAlreadyEnter = false;
    }

    void ComenzarLanzamiento()
    {
        propulsion.Play();

        canContinue = true;
        StartCoroutine(Avanzar());
    }

    IEnumerator Avanzar()
    {
        while (canContinue)
        {
            float XRelativeToCanon = target.position.x - InitialPos.x;
            float YRelativeToCanon = target.position.y - InitialPos.y;

            transform.up = new Vector2(XRelativeToCanon * -1, YRelativeToCanon * -1);

            float fixedSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);

            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.maxSpeed = 15;
            Invoke(nameof(ResetPlayerMaxSpeed), 0.5f);
            player.SendMessage("EnemyKnockBack", new float[] { transform.position.x, playerKnockBackPower });
        }
    }

    void ResetPlayerMaxSpeed()
    {
        player.maxSpeed = 3;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilController : MonoBehaviour
{
    
    public Transform target;
    public float playerKnockBackPower;
    public ParticleSystem propulsion;

    private float speed;
    private bool canContinue;
    public Transform Canon;

    void Start()
    {
        //Canon = GetComponentInParent<Transform>();
    }

    public void ComenzarLanzamiento(float speed)
    {
        this.speed = speed;
        propulsion.Play();

        canContinue = true;
        StartCoroutine(Avanzar());
    }

    IEnumerator Avanzar()
    {
        while (canContinue)
        {
            float XRelativeToCanon = target.position.x - Canon.position.x;
            float YRelativeToCanon = target.position.y - Canon.position.y;

            transform.up = new Vector2(XRelativeToCanon * -1, YRelativeToCanon * -1);

            float fixedSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);

            if (transform.position == target.position)
            {
                Destroy(gameObject);
            }

            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerController Player = col.GetComponentInParent<PlayerController>();
            Player.SendMessage("EnemyKnockBack", new float[] { transform.position.x, playerKnockBackPower });
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{

    public float angle;

    private Rigidbody2D rb2d;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        ThrowArrow2();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (rb2d != null)
        {
            transform.rotation = Quaternion.Euler(0,0,(rb2d.velocity.y * -3) * Mathf.Sign(transform.localScale.x));
        }
    }

    void ThrowArrow2()
    {
        Vector2 target = new Vector2(player.transform.position.x, player.transform.position.y + 0.5f);
        Vector2 force = calcForce(transform.position, target, angle) * 1.57f;

        rb2d.AddForce(force, ForceMode2D.Impulse);

        transform.SetParent(null);
    }

    Vector2 calcForce(Vector2 source, Vector2 target, float angle)
    {
        float fixedAngle = ((angle * 6) / Mathf.Clamp(Mathf.Abs(target.x - source.x), 6, 1000));
        fixedAngle += ((target.y - source.y) > 2) ? (((target.y - source.y) * 9) - (Mathf.Abs(target.x - source.x) / 1.5f)) : 0;

        Debug.Log(fixedAngle);

        Vector2 direction = target - source;
        float h = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        float a = fixedAngle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(a);
        distance += h / Mathf.Tan(a);

        // calculate velocity
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * direction.normalized;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        transform.parent = col.transform;
        Destroy(rb2d);
        Destroy(GetComponent<CircleCollider2D>());

        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.SendMessage("EnemyKnockBack", new float[] { transform.position.x, 10});
        }
    }
}

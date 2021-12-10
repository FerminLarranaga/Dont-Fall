using System.Collections;
using UnityEngine;

public class ArquerController : MonoBehaviour
{

    public float arquerSpeed, maxArquerSpeed, timeToThrow, timeToHit;

    [HideInInspector]
    public bool canMove;
    [HideInInspector]
    public bool canThrow;
    [HideInInspector]
    public bool canHit;

    private Animator anim;
    private Rigidbody2D rb2d;
    private GameObject player, Arrow;
    private AudioSource Audio;

    private float timeArrow = 0, timeHit = 0, timeBetweenHit = 1;
    private float direction = 1;
    private Vector3 initialLocalScale;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        Arrow = GetComponentsInChildren<Transform>(true)[1].gameObject;
        player = GameObject.Find("Player");
        initialLocalScale = transform.localScale;
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (canThrow)
            {
                timeArrow += Time.deltaTime;

                if (timeArrow >= timeToThrow)
                {
                    anim.SetBool("Throw", true);
                    Invoke("ThrowArrow", 0.31f);
                    timeArrow = 0;
                }
            } else if (canHit)
            {
                timeHit += Time.deltaTime;

                if (timeHit >= timeToHit)
                {
                    anim.SetBool("Hit", true);
                    Invoke("Hit", 0.2f);
                    timeHit = -timeBetweenHit;
                }
            }

            if (timeHit < 0)
            {
                timeHit += Time.deltaTime;
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 fixedVelocity = rb2d.velocity;
        fixedVelocity.x *= 0.75f;
        rb2d.velocity = fixedVelocity;

        if (player != null)
        {
            direction = Mathf.Sign(player.transform.position.x - transform.position.x);
            transform.localScale = new Vector3(initialLocalScale.x * -direction, transform.localScale.y, transform.localScale.z);

            if (canMove)
            {
                rb2d.AddForce(Vector2.right * arquerSpeed * direction);
                float fixedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxArquerSpeed, maxArquerSpeed);
                rb2d.velocity = new Vector2(fixedSpeed, rb2d.velocity.y);
            }
        }
        
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
    }

    void ThrowArrow()
    {
        GameObject arrow = Instantiate(Arrow, Arrow.transform.parent);
        arrow.SetActive(true);

        Audio.Play();
        anim.SetBool("Throw", false);
    }

    void Hit()
    {
        player.SendMessage("EnemyKnockBack", new float[] { transform.position.x, 10 });
        anim.SetBool("Hit", false);
    }
}

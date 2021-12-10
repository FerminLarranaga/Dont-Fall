using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private PlayerController player;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Ground") || col.CompareTag("Plataforma"))
        {
            player.SetGrounded(true);
            if (col.CompareTag("Plataforma"))
            {
                player.transform.parent = col.transform;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Plataforma"))
        {
            rb2d.velocity = new Vector3(0f, 0f, 0f);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ground") || col.CompareTag("Plataforma"))
        {
            player.transform.parent = null;
            player.SetGrounded(false);
        }
    }
}

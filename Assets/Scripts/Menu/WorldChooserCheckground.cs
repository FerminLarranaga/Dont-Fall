using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldChooserCheckground : MonoBehaviour
{
    private WorldChooserPlayer player;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<WorldChooserPlayer>();
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            player.SetGrounded(true);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            player.SetGrounded(false);
        }
    }
}

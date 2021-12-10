using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoostController : MonoBehaviour
{

    public Sprite JumpBoostActivated, JumpBoostDesactivated;
    public float JumpImpulse, HorizontalImpulse;

    private SpriteRenderer spr;
    private PlayerController player;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Rigidbody2D rb2dPlayer = col.GetComponentInParent<Rigidbody2D>();
            player = col.GetComponentInParent<PlayerController>();
            Animator animPlayer = col.GetComponentInParent<Animator>();

            rb2dPlayer.velocity = new Vector2(0, 0);
            rb2dPlayer.AddForce(Vector2.up * JumpImpulse, ForceMode2D.Impulse);

            //player.maxSpeed = 10;

            rb2dPlayer.AddForce(Vector2.right * (Mathf.Sign(player.transform.localScale.x) * HorizontalImpulse),  ForceMode2D.Impulse);
            player.SetDoubleJump(false);
            StartCoroutine("HandlePlayerAnim", animPlayer);

            spr.sprite = JumpBoostActivated;

            Invoke("DesactivateJumpBoost", 0.5f);
        }
    }

    void DesactivateJumpBoost()
    {
        spr.sprite = JumpBoostDesactivated;
        //player.maxSpeed = 3;
    }

    IEnumerator HandlePlayerAnim(Animator animPlayer)
    {
        float counter = 0f;
        while (counter < 0.1f){
            animPlayer.SetBool("Grounded", true);
            counter += 0.008f;
            yield return new WaitForSeconds(0.00000001f);
        }
    }
}

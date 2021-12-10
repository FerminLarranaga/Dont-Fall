using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalacancaController : MonoBehaviour
{

    public List<GameObject> BlocksOfPath = new List<GameObject>();
    public Sprite PalancaTocada;

    private SpriteRenderer PalancaSpr;
    private bool hasAlreadyBeenTouched = false;

    void Start()
    {
        PalancaSpr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space) && !hasAlreadyBeenTouched)
        {
            hasAlreadyBeenTouched = true;
            PalancaSpr.sprite = PalancaTocada;
            for (int i = 0; i < BlocksOfPath.Count; i++)
            {
                SpriteRenderer spr = BlocksOfPath[i].GetComponent<SpriteRenderer>();
                StartCoroutine(AppearBlock(spr));
            }
        }
    }

    IEnumerator AppearBlock(SpriteRenderer spr)
    {
        while (spr.color.a < 1)
        {
            float a = Mathf.Lerp(spr.color.a, 1, Time.deltaTime * 2);
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, a);
            yield return null;
        }
    }
}

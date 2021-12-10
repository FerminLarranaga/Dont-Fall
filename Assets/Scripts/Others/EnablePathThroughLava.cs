using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePathThroughLava : MonoBehaviour
{
    public List<GameObject> BlocksOfPath = new List<GameObject>();

    void OnCollisionEnter2D()
    {
        for (int i = 0; i < BlocksOfPath.Count; i++)
        {
            SpriteRenderer spr = BlocksOfPath[i].GetComponent<SpriteRenderer>();
            StartCoroutine(AppearBlock(spr));
        }
    }

    IEnumerator AppearBlock(SpriteRenderer spr)
    {
        for (float i = 0; i <= 255; i++)
        {
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, (i/255) * 3f);
            yield return new WaitForSeconds(0.000001f);
        }
    }
}

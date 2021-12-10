using System.Collections;
using UnityEngine;

public class PlatformPlayerRetractor : MonoBehaviour
{
    BoxCollider2D groundBoxPlayerCol;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (groundBoxPlayerCol == null)
                groundBoxPlayerCol = col.gameObject.GetComponentInChildren<BoxCollider2D>();

            groundBoxPlayerCol.enabled = false;

            Invoke("ActivateColliders", 0.5f);
        }
    }

    void ActivateColliders()
    {
        groundBoxPlayerCol.enabled = true;
    }
}

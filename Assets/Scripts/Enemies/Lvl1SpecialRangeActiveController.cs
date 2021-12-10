using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl1SpecialRangeActiveController : MonoBehaviour
{

    [HideInInspector]
    public bool HasAlreadyEnter;

    private Lvl1SpecialProjectilController projectil;

    void Start()
    {
        projectil = GetComponentInParent<Lvl1SpecialProjectilController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!HasAlreadyEnter)
        {
            if (col.CompareTag("Player"))
            {
                projectil.SendMessage("ComenzarLanzamiento");
                HasAlreadyEnter = true;
            }
        }
    }
}

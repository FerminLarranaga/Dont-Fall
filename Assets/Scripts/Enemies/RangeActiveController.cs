using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeActiveController : MonoBehaviour
{

    public GameObject Canon;

    public CanonHorizontalController CanonScript;
    private EdgeCollider2D ec2d;
    private bool HasAlreadyEnter;

    void Start()
    {
        ec2d = GetComponent<EdgeCollider2D>();
        //CanonScript = GetComponentInParent<CanonHorizontalController>();

        Vector2[] tempArray = ec2d.points;

        tempArray.SetValue(new Vector2(0, 0), 0);
        tempArray.SetValue(new Vector2(Mathf.Abs(transform.localPosition.x), Mathf.Abs(transform.localPosition.y)), 1);

        ec2d.points = tempArray;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!HasAlreadyEnter && !CanonScript.startWithoutTrigger)
        {
            if (col.CompareTag("Player"))
            {
                Canon.gameObject.SendMessage("ActivarCanon");
                HasAlreadyEnter = true;
            }
        }
    }
}

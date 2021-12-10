using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaMovementController2 : MonoBehaviour
{
    public Transform LavaBlock1, LavaBlock2;
    public float speed = 12;

    private float startX, endX;

    void Start()
    {
        startX = LavaBlock1.position.x;
        endX = LavaBlock2.position.x + 10;

        InvokeRepeating("LavaMovement", 0.2f, 0.1f);
    }

    void LavaMovement()
    {
        Vector3 P1, P2;
        P1 = LavaBlock1.position;
        P2 = LavaBlock2.position;

        LavaBlock1.position = new Vector3(P1.x + speed*Time.deltaTime, P1.y, P1.z);
        LavaBlock2.position = new Vector3(P2.x + speed*Time.deltaTime, P2.y, P2.z);

        if (P1.x >= endX)
        {
            LavaBlock1.position = new Vector3(startX + 0.3f, P1.y, P1.z);
        } else if (P2.x >= endX)
        {
            LavaBlock2.position = new Vector3(startX + 0.3f, P2.y, P2.z);
        }
    }
}

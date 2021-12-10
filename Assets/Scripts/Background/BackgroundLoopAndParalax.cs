using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoopAndParalax : MonoBehaviour
{
    public Transform cameraTransform;

    private Transform[] childrenT;
    private Vector3 lastCameraPosition;

    void Start()
    {
        childrenT = GetComponentsInChildren<Transform>();

        lastCameraPosition = cameraTransform.position;
    }

    void Update()
    {
        float xDist = cameraTransform.position.x - lastCameraPosition.x;
        lastCameraPosition = cameraTransform.position;

        Vector3 Minustarget = new Vector3(cameraTransform.position.x - 36f, cameraTransform.position.y);
        Vector3 Maxtarget = new Vector3(cameraTransform.position.x + 36f, cameraTransform.position.y);

        for (int i = 1; i < childrenT.Length; i++)
        {
            Transform t = childrenT[i];

            t.position = new Vector3(t.position.x + (xDist * t.position.z), t.position.y, t.position.z);

            if (t.position.x < Minustarget.x)
            {
                t.position = new Vector3((cameraTransform.position.x + 18f), t.position.y, t.position.z);
            } else if (t.position.x > Maxtarget.x)
            {
                t.position = new Vector3((cameraTransform.position.x - 18f), t.position.y, t.position.z);
            }
        }
    }
}
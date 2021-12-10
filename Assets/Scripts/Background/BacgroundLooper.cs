using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacgroundLooper : MonoBehaviour
{
    public Transform[] blocks;
    public Transform mainCamera;

    // [NO FUNCIONAA]
    void FixedUpdate()
    {
        foreach(Transform t in blocks)
        {
            if (t.position.x >= mainCamera.position.x + 36)
            {
                t.position = new Vector3(mainCamera.position.x - 18, t.position.y, t.position.z);
            } else if (t.position.x <= mainCamera.position.x - 36)
            {
                t.position = new Vector3(mainCamera.position.x + 18, t.position.y, t.position.z);
            }
        }
    }

    void LateUpdate()
    {
        int indexCenterT = 0;

        float distCamera = 100;

        for (int i = 0; i < blocks.Length; i++)
        {
            if (distCamera > mainCamera.position.x - blocks[i].position.x)
            {
                distCamera = mainCamera.position.x - blocks[i].position.x;
                indexCenterT = i;
            }
        }

        Debug.Log(distCamera);

        for (int i = 0; i < blocks.Length; i++)
        {
            if (i != indexCenterT)
            {
                Transform t = blocks[i];
                float xDist = Mathf.Abs(t.position.x - blocks[indexCenterT].position.x);
                float side = Mathf.Sign(t.position.x - blocks[indexCenterT].position.x);
                Debug.Log(t.name + ": " + side);
                if (xDist != 18)
                {
                    t.position = new Vector3(t.position.x + (blocks[indexCenterT].position.x + (18 * side)), t.position.y, t.position.z);
                }
            }
        }
    }
}

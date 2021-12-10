using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{

    public GameObject cam;
    public float ParalaxEffect;

    private float length, startX;
    private Vector3 StartPos;

    void Start()
    {
        startX = transform.position.x;
        length = 19.2f;
        StartPos = transform.localPosition;
    }

    void FixedUpdate()
    {
        float temp = Mathf.Abs(cam.transform.position.x * (1 - ParalaxEffect));
        float dist = Mathf.Abs(cam.transform.position.x * ParalaxEffect);

        transform.position = new Vector3(startX + dist, transform.position.y, transform.position.z);

        if (temp > startX + length) startX += length;
        else if (temp < startX - length) startX -= length;
    }

    void NewParalaxEffect()
    {
        
    }
}

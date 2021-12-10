using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMenu : MonoBehaviour
{

    public float speed = 4;

    private Transform[] backgrounds;
    private Vector2 target;

    void Start()
    {
        backgrounds = GetComponentsInChildren<Transform>();
        target = new Vector2(29.5f, 0);
    }
    
    void FixedUpdate()
    {
        for (int i = 1; i < backgrounds.Length; i++)
        {
            float fixedSpeed = speed * Time.deltaTime;
            backgrounds[i].position = Vector2.MoveTowards(backgrounds[i].position, target, fixedSpeed);

            if (backgrounds[i].position.x == target.x)
            {
                backgrounds[i].position = new Vector2(-29.5f, 0);
            }
        }
    }
}

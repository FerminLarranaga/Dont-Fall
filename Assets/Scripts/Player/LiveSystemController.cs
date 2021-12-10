using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveSystemController : MonoBehaviour
{

    public PlayerController Player;

    private Transform live;

    void Start()
    {
        live = GetComponentInChildren<SpriteRenderer>().GetComponent<Transform>();
        UpdateLives();
    }

    void UpdateLives()
    {
        if (Player != null)
        {
            int numberOfPlayerLives = Player.GetLives();

            if (numberOfPlayerLives > 0)
            {
                Transform[] lives = GetComponentsInChildren<Transform>();

                for (int i = 2; i < lives.Length; i++)
                {
                    Destroy(lives[i].gameObject);
                }

                float lastXPos = live.position.x;

                for (int i = 0; i < numberOfPlayerLives - 1; i++)
                {
                    Instantiate(live, new Vector3(lastXPos + 1.3f, live.position.y, live.position.z), new Quaternion(0, 0, 0, 0), live.parent);
                    lastXPos += 1.3f;
                }
            } else
            {
                Destroy(live.gameObject);
            }
        }
    }
}

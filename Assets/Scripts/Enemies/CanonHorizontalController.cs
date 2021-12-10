using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonHorizontalController : MonoBehaviour
{

    public float rateSpeed;
    public Sprite Activated, Desactivated;
    public GameObject projectil;
    public float speed = 10;
    public bool startWithoutTrigger = false;
    public float startAfter = 0.01f;

    private SpriteRenderer spr;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();

        if (startWithoutTrigger) ActivarCanon();
    }

    void ActivarCanon()
    {
        InvokeRepeating("LanzarProjectil", startAfter, rateSpeed);
    }

    public void DesactivarCanon()
    {
        CancelInvoke();
    }

    void LanzarProjectil()
    {
        spr.sprite = Activated;

        GameObject newProjectil = Instantiate(projectil, projectil.transform.parent);
        newProjectil.SendMessage("ComenzarLanzamiento", speed);

        Invoke("StopAnimation", 0.3f);
    }

    void StopAnimation()
    {
        spr.sprite = Desactivated;
    }
}

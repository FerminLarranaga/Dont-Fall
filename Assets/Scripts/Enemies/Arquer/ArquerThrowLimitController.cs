using UnityEngine;

public class ArquerThrowLimitController : MonoBehaviour
{

    private ArquerController Arquer;

    // Start is called before the first frame update
    void Start()
    {
        Arquer = GetComponentInParent<ArquerController>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (!Arquer.canHit)
                Arquer.canThrow = true;
            else
                Arquer.canThrow = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Arquer.canThrow = false;
        }
    }
}

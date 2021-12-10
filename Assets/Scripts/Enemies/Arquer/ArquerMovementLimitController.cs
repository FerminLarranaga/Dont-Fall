using UnityEngine;

public class ArquerMovementLimitController : MonoBehaviour
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
            if (!Arquer.canThrow && !Arquer.canHit)
                Arquer.canMove = true;
            else
                Arquer.canMove = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Arquer.canMove = false;
        }
    }
}

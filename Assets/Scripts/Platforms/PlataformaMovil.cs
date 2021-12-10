using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    public Transform target;
    public float speed;
    public bool startWithNoPlayer = false;

    private Vector3 start, end;
    private bool IsActive = false;
    private PlayerController player;
    private int playerLives = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
        {
            target.parent = null;
            start = transform.position;
            end = target.position;
        }

        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        if (IsActive || transform.position != start || startWithNoPlayer)
        {
            if (target != null)
            {
                float fixedSpeed = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);
            }

            if (target.position == transform.position)
            {
                target.position = (target.position == start) ? end : start;
            }
        }

        HandlePlayerRespawn();
    }

    void HandlePlayerRespawn()
    {
        if (player != null)
        {
            if (playerLives == 0)
            {
                playerLives = player.GetLives();
            }

            if (player.GetLives() != playerLives)
            {
                playerLives = player.GetLives();
                transform.position = start;
                target.position = end;
            }
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (!IsActive)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                IsActive = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        IsActive = false;
    }
}

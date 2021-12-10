using UnityEngine;

public class HandleAttackRange : MonoBehaviour
{

    private PlayerController Player;

    void Start()
    {
        Player = GetComponentInParent<PlayerController>();
    }

    // Cuando haya algo colisionando con el rango de ataque
    void OnTriggerStay2D(Collider2D col)
    {
        // Si la colision es un enemigo y el player ataca
        if (col.CompareTag("Enemy") && Player.GetAttack())
        {
            Player.StartCoroutine(Player.DestroyEnemy(col));
        }
    }
}

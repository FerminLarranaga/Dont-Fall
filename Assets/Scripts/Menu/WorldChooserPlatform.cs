using UnityEngine;
using UnityEngine.UI;

public class WorldChooserPlatform : MonoBehaviour
{
    public Button btn;
    public Animator Character;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            btn.interactable = true;
            btn.Select();
            Character.SetBool("isRunning", true);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        btn.interactable = false;
        Character.SetBool("isRunning", false);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class MenuBtnsController : MonoBehaviour
{

    public AudioSource ClickSound, BGMusic;
    public GameObject Menu, WorldChooser, OptionsMenu;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void StartGame()
    {
        Menu.SetActive(false);
        WorldChooser.SetActive(true);
        ClickSound.Play();
    }

    public void OpenOptions(Button btn)
    {
        btn.interactable = false;
        OptionsMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        ClickSound.Play();
    }

    public void CloseOptionsMenu(Button btn)
    {
        if (OptionsMenu.activeSelf)
        {
            btn.interactable = true;
            OptionsMenu.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            ClickSound.Play();
        }
    }

    public void Exit()
    {
        Application.Quit();
        ClickSound.Play();
    }
}

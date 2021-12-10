using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    public GameObject PauseMenu, OptionsMenu;
    public PauseMenuArrow ResumeArrow;

    private AudioSource BGMusic;
    private PlayerController Player;
    private AudioSource Click;

    void Start()
    {
        Click = GetComponent<AudioSource>();
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        BGMusic = GameObject.Find("BGMusic").GetComponent<AudioSource>();
    }

    public void Resume()
    {
        Click.Play();
        StartCoroutine(disablePauseMenu());
    }

    IEnumerator disablePauseMenu()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.1f);
        PauseMenu.SetActive(false);
        BGMusic.volume = 0.25f;
        ResumeArrow.fixingBug = 0;
    }

    public void MainMenu()
    {
        Player.SaveWorld();
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
        Click.Play();
    }

    public void Options()
    {
        OptionsMenu.SetActive(true);
        Click.Play();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseOptionsMenu()
    {
        if (OptionsMenu.activeSelf)
        {
            OptionsMenu.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Click.Play();
        }
    }

    public void Save()
    {
        Player.SaveWorld();
        Click.Play();
    }
}

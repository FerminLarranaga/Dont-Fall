using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationBehaviourController : MonoBehaviour
{
    private PlayerController Player;

    private void Start() {
        GetPlayer();

        DontDestroyOnLoad(gameObject);
    }
    void OnApplicationQuit()
    {
        if (Player != null && SavingSystem.currentWorld != 0)
        {
            Player.SaveWorld();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GetPlayer();
    }

    private void GetPlayer()
    {
        try
        {
            Player = GameObject.Find("Player").GetComponent<PlayerController>();
        }
        catch (System.Exception e)
        {
            Debug.Log("Unable to find Player, error: " + e.Message);
        }
    }
}

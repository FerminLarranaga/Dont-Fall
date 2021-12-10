using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackDoorController : MonoBehaviour
{

    public string nextScene;

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                /*PlayerController Player = col.GetComponentInParent<PlayerController>();
                Player.SaveLevel();
                WorldData data = new WorldData(1, new float[2] { 0, 0 }, level: Player.GetLevel(),
                                               lives: Player.GetLives());
                SavingSystem.SaveWorld(data, SavingSystem.WorldDataPath);*/

                string DataFolderPath = Application.persistentDataPath + SavingSystem.WorldFolderPath;

                string[] dataFilesPaths = Directory.GetFiles(DataFolderPath);
                foreach (string dataFilePath in dataFilesPaths)
                {
                    File.Delete(dataFilePath);
                }

                Directory.Delete(DataFolderPath);

                SceneManager.LoadScene(nextScene);
            }
        }
    }
}

using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldChooserController : MonoBehaviour
{
    public void CreateNewWorld(int indexOfWorld)
    {
        SavingSystem.UpdatePaths(indexOfWorld);

        if (!File.Exists(Application.persistentDataPath + SavingSystem.WorldDataPath))
        {
            Directory.CreateDirectory(Application.persistentDataPath + SavingSystem.WorldFolderPath);

            WorldData playerData = new WorldData(1, new float[] {-7, -2});

            SavingSystem.SaveWorld(playerData, SavingSystem.WorldDataPath);
        }

        SceneManager.LoadScene("level" + SavingSystem.LoadWorld(SavingSystem.WorldDataPath).level);
    }
}

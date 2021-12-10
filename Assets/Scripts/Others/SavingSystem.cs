using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SavingSystem
{

    public static int currentWorld = 1;
    public static string WorldFolderPath = "/World " + currentWorld;
    public static string WorldDataPath = WorldFolderPath + "/WorldData.gsp";

    public static void UpdatePaths(int newCurrentWorld)
    {
        currentWorld = newCurrentWorld;
        WorldFolderPath = "/World " + currentWorld;
        WorldDataPath = WorldFolderPath + "/WorldData.gsp";
    }

    public static void SaveLevel(LevelData data, string pathEnding)
    {
        string path = UnityEngine.Application.persistentDataPath + pathEnding;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveWorld(WorldData data, string pathEnding)
    {
        string path = UnityEngine.Application.persistentDataPath + pathEnding;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static LevelData LoadLevel(string pathEnding)
    {
        string path = UnityEngine.Application.persistentDataPath + pathEnding;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        }
        else
        {
            UnityEngine.Debug.LogError("Save file not found in: " + path);
            return null;
        }
    }

    public static WorldData LoadWorld(string pathEnding)
    {
        string path = UnityEngine.Application.persistentDataPath + pathEnding;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            WorldData data = formatter.Deserialize(stream) as WorldData;
            stream.Close();

            return data;
        }
        else
        {
            UnityEngine.Debug.LogError("Save file not found in: " + path);
            return null;
        }
    }
}
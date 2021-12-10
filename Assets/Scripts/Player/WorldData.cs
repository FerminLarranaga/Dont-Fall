using System.IO;

[System.Serializable]
public class WorldData
{

    public int level, lives, checkpointsTaken;
    public int totalDeaths, enemiesKilled, totalJumps, difficulty;
    public float totalTime;
    public float[] respawnPoint;


    public WorldData(int[] intsData, float totalTime, float[] respawnPoint)
    {
        LoadData();
        level = intsData[0];
        lives = intsData[1];

        checkpointsTaken += intsData[2];
        totalDeaths += intsData[3];
        enemiesKilled += intsData[4];
        totalJumps += intsData[5];
        this.totalTime += totalTime;

        difficulty = intsData[6];
        this.respawnPoint = respawnPoint;
    }

    public WorldData(int difficulty, float[] respawnPoint, int level = 1, int lives = 5, int checkpointsTaken = 0, 
                     int totalDeaths = 0, int enemiesKilled = 0, int totalJumps = 0, float totalTime = 0)
    {
        LoadData();
        this.level = level;
        this.lives = lives;

        this.checkpointsTaken += checkpointsTaken;
        this.totalDeaths += totalDeaths;
        this.enemiesKilled += enemiesKilled;
        this.totalJumps += totalJumps;
        this.totalTime += totalTime;

        this.difficulty = difficulty;
        this.respawnPoint = respawnPoint;
    }

    void LoadData()
    {
        if (File.Exists(UnityEngine.Application.persistentDataPath + SavingSystem.WorldDataPath))
        {
            WorldData data = SavingSystem.LoadWorld(SavingSystem.WorldDataPath);
            checkpointsTaken = data.checkpointsTaken;
            totalDeaths = data.totalDeaths;
            enemiesKilled = data.enemiesKilled;
            totalJumps = data.totalJumps;
            totalTime = data.totalTime;
        }
    }
}

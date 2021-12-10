
[System.Serializable]
public class LevelData
{
    public int totalJumps, deaths, checkpointsTaken;
    public float timePlayed;

    public LevelData(PlayerController Player)
    {
        totalJumps = Player.GetTotalJumps();
        deaths = Player.GetDeaths();
        checkpointsTaken = Player.GetCheckpointsTaken();
        timePlayed = Player.GetTimePlayed();
    }

    public LevelData(int totalJumps, int deaths, int checkpointsTaken, float timePlayed)
    {
        this.totalJumps = totalJumps;
        this.deaths = deaths;
        this.checkpointsTaken = checkpointsTaken;
        this.timePlayed = timePlayed;
    }
}

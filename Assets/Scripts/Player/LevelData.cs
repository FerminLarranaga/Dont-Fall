using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level, lives;
    public float[] respawnPoint;

    public PlayerData(PlayerController Player)
    {
        level = Player.GetLevel();
        lives = Player.GetLives();

        float[] pos = new float[2] { Player.GetRespawnPoint().x, Player.GetRespawnPoint().y };

        respawnPoint = pos;
    }

    public PlayerData(int level, int lives, float[] pos)
    {
        this.level = level;
        this.lives = lives;

        respawnPoint = pos;
    }
}

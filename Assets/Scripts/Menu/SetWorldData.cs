using UnityEngine;
using System.IO;
using TMPro;

public class SetWorldData : MonoBehaviour
{

    public int indexWorld;

    public TextMeshProUGUI time, level, lives;

    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/World " + indexWorld + "/WorldData.gsp"))
        {
            WorldData data = SavingSystem.LoadWorld("/World " + indexWorld + "/WorldData.gsp");

            string minutesTime = ((int)(data.totalTime / 60)).ToString();
            string secondsTime = ((int)(data.totalTime - (int.Parse(minutesTime) * 60))).ToString();
            string milisecondsTime = data.totalTime.ToString().Split(',')[1];

            if (milisecondsTime.Length > 2)
            {
                milisecondsTime = milisecondsTime.Remove(2);
            }

            string timePlayed = minutesTime + ":" + secondsTime + ":" + milisecondsTime;
            time.text = timePlayed;
            level.text = data.level.ToString();
            lives.text = data.lives.ToString();
        }
    }
}

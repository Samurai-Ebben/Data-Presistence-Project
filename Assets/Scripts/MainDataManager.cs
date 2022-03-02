using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class MainDataManager : MonoBehaviour
{
    public static MainDataManager Instance;

    public  InputField Name;
    public string PlayerName;
    public TextMeshProUGUI bestScore;
    public int score;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadPlayerInfo();
        bestScore.text = "Best Score: " + PlayerName + " : "+ score;
    }

    public void SetPlayerName()
    {
        PlayerName = Name.text;
        SavePlayerInfo();
    }

    [System.Serializable]
    class PlayerInfo
    {
        public int HighScore;
        public string PlayerName;

    }

    public void SavePlayerInfo()
    {
        PlayerInfo playerData = new PlayerInfo();
        playerData.HighScore = score;
        playerData.PlayerName = PlayerName;

        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerInfo playerData = JsonUtility.FromJson<PlayerInfo>(json);

            score = playerData.HighScore;
            PlayerName = playerData.PlayerName;
        }
    }

}

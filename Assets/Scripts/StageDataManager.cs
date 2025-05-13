using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.CompilerServices;

[System.Serializable]

public class StageResult
{
    public string playerName;

    public int stage;

    public int score;
}
[System.Serializable]

public class StageResultilist
{
    public List<StageResult> results = new List<StageResult>();
}

public static class StageResultSaver
{
    private const string FILE = "stage_result.json";
    private const string PLAYER_NAME = "PlayerName";
    private static string filePath = Path.Combine(Application.persistentDataPath, FILE);

    public static void SaveStage(int stage, int score)
    {
        StageResultilist list = LoadInternal();
        string playerName = PlayerPrefs.GetString(PLAYER_NAME, "");
        StageResult entry = new StageResult
        {
            playerName = playerName,
            stage = stage,
            score = score
        };
        list.results.Add(entry);
        string json = JsonUtility.ToJson(list, true);
        File.WriteAllText(filePath, json);
    }
    public static StageResultilist LoadRank()
    {
        return LoadInternal();
    }

    private static StageResultilist LoadInternal()
    {
        if (!File.Exists(filePath))
            return new StageResultilist();
        string json = File.ReadAllText(filePath);
        StageResultilist list = JsonUtility.FromJson<StageResultilist>(json);
        if (list == null)
            return new StageResultilist();
        else
            return list;
    }
}

public class StageDataManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

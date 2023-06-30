using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System;
using System.IO;
using System.Text;

public static class SaveLoadManager
{
    private const string DATA_PATH = "player_data";

    private static PlayerData _data = null;



    public static void AddCoin()
    {
        _data.coins++;
    }

    public static int GetCoinsCount()
    {
        return _data.coins;
    }


    
    public static void LoadData()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        string path = Path.Combine(Application.dataPath, DATA_PATH);

#elif UNITY_ANDROID
        string path = Path.Combine(Application.persistentDataPath, DATA_PATH);

#else
        string path = Path.Combine(Application.dataPath, DATA_PATH);

#endif

        if (File.Exists(path)) 
        {
            try
            {
                string json = File.ReadAllText(path);

                _data = JsonUtility.FromJson<PlayerData>(json);
            }
            catch
            {
                _data = new PlayerData();
            }
        }
        else
        {
            _data = new PlayerData();
        }
    }

    public static void SaveData()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        string path = Path.Combine(Application.dataPath, DATA_PATH);

#elif UNITY_ANDROID
        string path = Path.Combine(Application.persistentDataPath, DATA_PATH);

#else
        string path = Path.Combine(Application.dataPath, DATA_PATH);

#endif

        if(_data != null)
        {
            File.WriteAllText(path, JsonUtility.ToJson(_data, true));
        }
    }
}
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

    public static void SetSelected(int selected)
    {
        _data.selected = selected;
    }

    public static int GetSelected()
    {
        return _data.selected;
    }


    
    public static void LoadData()
    {
        if (PlayerPrefs.HasKey(DATA_PATH)) 
        {
            try
            {
                string json = PlayerPrefs.GetString(DATA_PATH);

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
        if(_data != null)
        {
            PlayerPrefs.SetString(DATA_PATH, JsonUtility.ToJson(_data, true));
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int coins;

    public int selected;



    public PlayerData()
    {
        coins = 0;

        selected = 0;
    }
}

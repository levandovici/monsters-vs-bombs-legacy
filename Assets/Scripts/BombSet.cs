using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BombSet
{
    [SerializeField]
    private Bomb _bomb;

    [SerializeField][Range(0.001f, 1f)]
    private float _rarity;

    [SerializeField]
    private int _count = 0;



    public Bomb Bomb => _bomb;

    public float Rarity => _rarity;

    public int Count
    {
        get
        {
            return _count;
        }

        set
        {
            _count = value;
        }
    }

    public float Weight => Count * Rarity;
}

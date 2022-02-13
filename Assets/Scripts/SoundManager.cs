using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _music;

    [SerializeField]
    private AudioSource _sfx;

    [SerializeField]
    private AudioClip[] _collect_coin;

    [SerializeField]
    private AudioClip[] _explosion;



    public void PlaySfx(ESfx sfx)
    {
        switch(sfx)
        {
            case ESfx.collect_coin:
                _sfx.PlayOneShot(_collect_coin[UnityEngine.Random.Range(0, _collect_coin.Length)]);
                break;

            case ESfx.explosion:
                _sfx.PlayOneShot(_explosion[UnityEngine.Random.Range(0, _explosion.Length)]);
                break;

            default:
                throw new NotImplementedException();
        }
    }



    public enum ESfx
    {
        collect_coin, explosion,
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _sprite_renderer;


    [SerializeField]
    private float _max_health = 100f;

    [SerializeField]
    private float _health;



    public event Action OnCollectCoin;

    public event Action OnDamage;



    public SpriteRenderer SpriteRenderer => _sprite_renderer;

    public float Health
    {
        get
        {
            return _health;
        }
    }

    public float MaxHealth
    {
        get
        {
            return _max_health;
        }
    }



    private void Awake()
    {
        _sprite_renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }



    private void Start()
    {
        _health = _max_health;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            Destroy(collision.gameObject);

            OnCollectCoin.Invoke();
        }
    }



    public void Damage(float amount)
    {
        _health = Mathf.Clamp(_health - amount, 0f, _health);

        OnDamage.Invoke();
    }
}

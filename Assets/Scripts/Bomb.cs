using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private Vector2 _world_x_destroy_positions = new Vector2(-128f, 128f);

    [SerializeField]
    private float _damage = 7f;

    [SerializeField]
    private GameObject _explosion;


    [SerializeField]
    private GameObject _fresh_candle_wick;

    [SerializeField]
    private GameObject _burning_candle_wick;

    [SerializeField]
    private float _fall_speed = 20f;

    [SerializeField]
    private bool _fall = true;

    [SerializeField]
    private float _move_speed = 7f;

    [SerializeField]
    private float _rotate_speed = 3f;

    [SerializeField]
    private bool _move = false;



    private void Start()
    {
        SetUpCandlewick(true);
    }



    private void Update()
    {
        if(_fall)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -13f, 0f), _fall_speed * Time.deltaTime);
        }
        else if (_move)
        {
            Vector3 target = Vector3.zero;

            bool move_left = transform.position.x < 0f ? true : transform.position.x > 0f ? false : UnityEngine.Random.Range(0, 2) == 0 ? true : false;

            if (move_left)
            {
                target = new Vector3(_world_x_destroy_positions.x, transform.position.y, 0f);

                transform.position = Vector3.MoveTowards(transform.position, target, _move_speed * Time.deltaTime);

                transform.Rotate(new Vector3(0f, 0f, _rotate_speed) * Time.deltaTime);
            }
            else
            {
                target = new Vector3(_world_x_destroy_positions.y, transform.position.y, 0f);

                transform.position = Vector3.MoveTowards(transform.position, target, _move_speed * Time.deltaTime);

                transform.Rotate(new Vector3(0f, 0f, -_rotate_speed) * Time.deltaTime);
            }

            if(Vector3.Distance(transform.position, target) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            SetUpCandlewick(false);
        }
        else if(_fall && collision.tag == "Player")
        {
            Player player = collision.attachedRigidbody.GetComponent<Player>();

            player.Damage(_damage);

            Instantiate(_explosion, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    private void SetUpCandlewick(bool candlewick)
    {
        _fall = candlewick;

        _move = !candlewick;

        _fresh_candle_wick.SetActive(!candlewick);

        _burning_candle_wick.SetActive(candlewick);
    }
}

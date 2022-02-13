using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField]
    private Vector2 _screen_size;

    [SerializeField]
    private float _screen_x_borders_percentage = 5f;

    [SerializeField]
    private Vector2 _screen_x_borders;

    [SerializeField]
    private Vector2 _world_x_borders;


    [SerializeField]
    private SoundManager _sound_manager;

    [SerializeField]
    private Animator _game_ui;

    [SerializeField]
    private GameObject[] _backgrounds;

    [SerializeField]
    private Slider _health_bar;

    [SerializeField]
    private Text _health_text;

    [SerializeField]
    private Text _coins_text;

    [SerializeField]
    private int _coins = 0;

    [SerializeField]
    private BombSet[] _bombs;

    [SerializeField]
    private Vector2 _new_bomb_after = new Vector2(1f, 5f);

    [SerializeField]
    private float _last_bomb = 0f;

    [SerializeField]
    private float _next_bomb = 3f;

    [SerializeField]
    private Coin _coin;

    [SerializeField]
    private Vector2 _new_coin_after = new Vector2(1f, 3f);

    [SerializeField]
    private float _last_coin = 0f;

    [SerializeField]
    private float _next_coin = 7f;



    [SerializeField]
    private Player _player;

    [SerializeField]
    private Transform _objects;

    private bool _game_over = false;

    [SerializeField]
    private float _move_speed = 5f;

    [SerializeField]
    private bool _move_left = false;



    private void Start()
    {
        _screen_size = new Vector2(Screen.width, Screen.height);

        _screen_x_borders.x = _screen_size.x * 0.01f * _screen_x_borders_percentage;

        _screen_x_borders.y = _screen_size.x - _screen_size.x * 0.01f * _screen_x_borders_percentage;

        _world_x_borders.x = Camera.main.ScreenToWorldPoint(new Vector3(_screen_x_borders.x, 0f, 0f)).x;

        _world_x_borders.y = Camera.main.ScreenToWorldPoint(new Vector3(_screen_x_borders.y, 0f, 0f)).x;


        _move_left = UnityEngine.Random.Range(0, 2) == 0 ? true : false;

        _player.OnCollectCoin += () =>
        {
            _sound_manager.PlaySfx(SoundManager.ESfx.collect_coin);

            _coins++;
        };

        _player.OnDamage += () =>
        {
            _sound_manager.PlaySfx(SoundManager.ESfx.explosion);

            if(_player.Health <= 0f)
            {
                GameOver();
            }
        };
    }



    private void Update()
    {
        if (!_game_over)
        {
#if UNITY_EDITOR || !UNITY_ANDROID
            if (_screen_size.x != Screen.width || _screen_size.y != Screen.height)
            {
                _screen_size = new Vector2(Screen.width, Screen.height);

                _screen_x_borders.x = _screen_size.x * 0.01f * _screen_x_borders_percentage;

                _screen_x_borders.y = _screen_size.x - _screen_size.x * 0.01f * _screen_x_borders_percentage;

                _world_x_borders.x = Camera.main.ScreenToWorldPoint(new Vector3(_screen_x_borders.x, 0f, 0f)).x;

                _world_x_borders.y = Camera.main.ScreenToWorldPoint(new Vector3(_screen_x_borders.y, 0f, 0f)).x;
            }
#endif

            _health_bar.value = _player.Health / _player.MaxHealth;

            _health_text.text = $"{_player.Health}/{_player.MaxHealth}";

            _coins_text.text = $"{_coins}";

            if (Input.GetMouseButtonDown(0))
            {
                _move_left = !_move_left;
            }

            Vector3 target = new Vector3(_move_left ? _world_x_borders.x : _world_x_borders.y, _player.transform.position.y, 0f);

            _player.transform.position = Vector3.MoveTowards(_player.transform.position, target, _move_speed * Time.deltaTime);

            if (Vector3.Distance(_player.transform.position, target) < 0.1f)
            {
                _move_left = !_move_left;
            }

            _player.SpriteRenderer.flipX = !_move_left;

            if (Time.time > _next_bomb)
            {
                _last_bomb = Time.time;

                _next_bomb = _last_bomb + UnityEngine.Random.Range(_new_bomb_after.x, _new_bomb_after.y);

                Bomb bomb = Instantiate(GetBombPrefab(), new Vector3(UnityEngine.Random.Range(_world_x_borders.x, _world_x_borders.y), 32f, 0f), Quaternion.identity, _objects);
            }

            if (Time.time > _next_coin)
            {
                _last_coin = Time.time;

                _next_coin = _last_coin + UnityEngine.Random.Range(_new_coin_after.x, _new_coin_after.y);

                Coin coin = Instantiate(_coin, new Vector3(UnityEngine.Random.Range(_world_x_borders.x, _world_x_borders.y), 32f, 0f), Quaternion.identity, _objects);
            }
        }
    }

    private Bomb GetBombPrefab()
    {
        int min_weight_index = -1;

        float weight = float.MaxValue;

        for(int i = 0; i < _bombs.Length; i++)
        {
            if(_bombs[i].Weight < weight)
            {
                min_weight_index = i;

                weight = _bombs[i].Weight;
            }
        }

        _bombs[min_weight_index].Count++;

        return _bombs[min_weight_index].Bomb;
    }

    private void GameOver()
    {
        _health_bar.value = _player.Health / _player.MaxHealth;

        _health_text.text = $"{_player.Health}/{_player.MaxHealth}";

        _game_over = true;

        _game_ui.SetBool("Over", true);

        Destroy(_player.gameObject);

        for(int i = 0; i < _objects.childCount; i++)
        {
            Destroy(_objects.GetChild(i).gameObject);
        }
    }
}
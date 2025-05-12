using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    private const string PRIVACY_POLICY = @"https://games.limonadoent.com/privacy-policy.html";


    [SerializeField]
    private Button _play;

    [SerializeField]
    private Text _coins;

    [SerializeField]
    private Button _privacy;

    [SerializeField]
    private Image _player;

    [SerializeField]
    private Sprite[] _monsters;

    [SerializeField]
    private int _selected = 0;

    [SerializeField]
    private Button _left;

    [SerializeField]
    private Button _right;



    private void Awake()
    {
        SaveLoadManager.LoadData();

        _play.onClick.AddListener(Play);

        _privacy.onClick.AddListener(() =>
        {
            Application.OpenURL(PRIVACY_POLICY);
        });

        _left.onClick.AddListener(Left);

        _right.onClick.AddListener(Right);
    }

    private void Start()
    {
        _coins.text = SaveLoadManager.GetCoinsCount().ToString();

        _selected = SaveLoadManager.GetSelected();

        _player.sprite = _monsters[_selected];
    }

    private void Play()
    {
        SceneManager.LoadScene(1);

        SaveLoadManager.SaveData();
    }

    private void Left()
    {
        _selected--;

        if(_selected < 0)
        {
            _selected = 0;
        }

        _player.sprite = _monsters[_selected];

        SaveLoadManager.SetSelected(_selected);
    }

    private void Right()
    {
        _selected++;

        if(_selected >= _monsters.Length)
        {
            _selected = _monsters.Length - 1;
        }

        _player.sprite = _monsters[_selected];

        SaveLoadManager.SetSelected(_selected);
    }
}

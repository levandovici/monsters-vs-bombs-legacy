using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField]
    private Button _play;

    [SerializeField]
    private Text _coins;



    private void Awake()
    {
        SaveLoadManager.LoadData();

        _play.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
    }

    private void Start()
    {
        _coins.text = SaveLoadManager.GetCoinsCount().ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    private const string PRIVACY_POLICY = @"https://limonado-entertainment.jimdosite.com/privacy.policy/";


    [SerializeField]
    private Button _play;

    [SerializeField]
    private Text _coins;

    [SerializeField]
    private Button _privacy;



    private void Awake()
    {
        SaveLoadManager.LoadData();

        _play.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });

        _privacy.onClick.AddListener(() =>
        {
            Application.OpenURL(PRIVACY_POLICY);
        });
    }

    private void Start()
    {
        _coins.text = SaveLoadManager.GetCoinsCount().ToString();
    }
}

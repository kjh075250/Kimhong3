using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tutorialObj;
    [SerializeField]
    private Button startBtn;
    [SerializeField]
    private Button settingBtn;
    [SerializeField]
    private Button exitBtn;
    [SerializeField]
    private GameObject settingScene;
    [SerializeField]
    private GameObject exitPn;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void GameStart()
    {

    }
    private void SettingOn()
    {
        settingScene.SetActive(true);
    }
    private void ExitGame()
    {
        exitPn.SetActive(true);
    }
}

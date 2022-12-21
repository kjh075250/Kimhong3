using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject controlPanel;
    [SerializeField]
    private GameObject exitPanel;

    public void ClickPlay()
    {
        SceneManager.LoadScene("KjhScene");
    }
    public void ClickExit()
    {
        exitPanel.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void CancelExit()
    {
        exitPanel.SetActive(false);
    }
    public void ClickControl()
    {
        controlPanel.SetActive(true);
    }
    public void ClickControlExit()
    {
        controlPanel.SetActive(false);
    }
}

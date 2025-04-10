using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private GameObject endWindow;
    [SerializeField]
    private TMP_Text finalText;

    [SerializeField]
    private List<string> winTextList = new List<string>();
    [SerializeField]
    private List<string> loseTextList = new List<string>();
    private void Awake()
    {
        Instance = this;
    }

    public void ShowWinScreen()
    {
        finalText.text = winTextList[Random.Range(0, winTextList.Count)];
        endWindow.SetActive(true);
    }
    public void ShowLoseScreen()
    {
        finalText.text = loseTextList[Random.Range(0, winTextList.Count)];
        endWindow.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using ColorCrowd;

public class Canvas : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public GameObject Privacy_Policy;
    int levelNo;
    public static Canvas instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        levelNo = SceneManager.GetActiveScene().buildIndex;

        levelText.text = "Level " + levelNo.ToString();
        Privacy_Policy.SetActive(false);
    }

    public void NextLevel()
    {
        ShowAd();
        Invoke(nameof(LoadNextLevel), 1f);
    }
    public void LoadNextLevel()
    {
        levelNo++;
        if (levelNo > 50)
        {
            levelNo = 1;
        }
        PlayerPrefs.SetInt("Level", levelNo);
        SceneManager.LoadScene(levelNo);
    }

    public void Reload()
    {
        ShowAd();
        Invoke(nameof(ReloadScene), 1f);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(levelNo);
    }
    public void PrivacyPolicy()
    {
        Application.OpenURL("https://unconditionalgames.blogspot.com/2020/01/privacy-policy-this-privacy-policy.html");
    }
    public void Activate_Privacy_Policy()
    {
        Privacy_Policy.SetActive(true);
    }
    public void deactivate_PP()
    {
        Privacy_Policy.SetActive(false);
    }
    public void MainMenu()
    {
        ShowAd();
        Invoke(nameof(LoadMainMenu), 1f);
    }
    public void ShowAd()
    {
        IronSourceAds.Instance.ShowFullScreen();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("1. Main scene");
    }
}

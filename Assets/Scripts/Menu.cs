using System;
using System.Collections;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] ButtonManager NewGame_Button;
    [SerializeField] ButtonManager Continue_Button;
    [SerializeField] ButtonManager Settings_Button;
    [SerializeField] ButtonManager Shop_Button;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject ShopMenu;
    public static Menu Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        NewGame_Button.onClick.AddListener(NewGame);
        Continue_Button.onClick.AddListener(Continue);
        Settings_Button.onClick.AddListener(Settings);
        Shop_Button.onClick.AddListener(Shop);
        if (PlayerPrefs.GetInt("GameStart", 0) == 0)
        {
            NewGame_Button.gameObject.SetActive(true);
            Continue_Button.gameObject.SetActive(false);
        }
        else
        {
            NewGame_Button.gameObject.SetActive(false);
            Continue_Button.gameObject.SetActive(true);
        }
    }

    private void NewGame()
    {
        SceneManager.LoadSceneAsync(1);
        PlayerPrefs.SetInt("GameStart", 1);
    }

    private void Continue()
    {
        SceneManager.LoadSceneAsync(1);
    }

    private void Settings()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    private void Shop()
    {
        MainMenu.SetActive(false);
        ShopMenu.SetActive(true);
    }

    public void MenuOpen()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        ShopMenu.SetActive(false);
    }
}

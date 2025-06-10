using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] private int Money;
     public static GameData Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        LoadData();
    }

    public void LoadData()
    {
        Money = PlayerPrefs.GetInt("Money" , 0);
    }

    public void SaveUpgrade(WeaponType weapon, int level)
    {
        PlayerPrefs.SetInt("Upgrade_" + weapon.ToString(), level);
    }

    public int LoadUpgrade(WeaponType weapon)
    {
        return PlayerPrefs.GetInt("Upgrade_" + weapon.ToString(), 0); 
    }
    
    public int GetMoney()
    {
        return Money;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
       // GameAnalytics.Initialize();

        if (PlayerPrefs.GetInt("Played", 0) == 0)
        {
            SceneManager.LoadScene(1);
            PlayerPrefs.SetInt("Played", 1);

        }
        else
            LoadLastScene();
    }


    private static void LoadLastScene()
    {
        SceneManager.LoadScene("Main");

    }

    public static void LoadLevelCount()
    {
        PlayerPrefs.GetInt("current_scene");

    }
}

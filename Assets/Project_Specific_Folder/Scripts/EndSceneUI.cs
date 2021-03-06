using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class EndSceneUI : MonoBehaviour
{
    Scene currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }
    public void Reset1()
    {
        DOTween.KillAll();
      
        string sceneName = currentScene.name;
        // PlayerPrefs.SetInt("SaveScene", SceneManager.GetActiveScene().buildIndex );

        if (sceneName == "Steven Main 7")
        {
            SceneManager.LoadScene(01);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }

    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

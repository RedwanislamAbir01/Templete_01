using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
public class EndSceneUI : MonoBehaviour
{
    Scene currentScene;
    public TMP_Text MultiplyText;
    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }


    private void OnEnable()
    {
        MultiplyText.text = "X" +GameManager.Instance.WallCollidedWith.ToString();

       UiManager.Instance.CurrentScoreUpdater();
    }


    public void Reset1()
    {
        DOTween.KillAll();
        UiManager.Instance.SetTotalScore();
        UiManager.Instance.UpgradePnael.SetActive(true);

    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

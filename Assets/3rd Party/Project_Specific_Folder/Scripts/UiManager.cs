using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class UiManager : Singleton<UiManager>
{
    public TMP_Text LevelText, CoinText, PointText, totalText, NormalCoin, BonusText;
    public int TotalScore;
    public int RewardValue;
    public Text Multiplied;
    int RewardMultiplyValue = 0;
    [SerializeField] int TotalCoinsCount;
    int currentLevel;
    int currentLevelText;

    public GameObject StartUI, EndUi, CompleteUI , FadeIn , UpgradePnael;
    public GameObject TapFastPanel;

    public GameObject fillbarTimer;
    public Image Timer;
    public float timerInitvalue;
    int _currentLevel;
    public override void Start()
    {
        base.Start();
        GetTotalScore();
        _currentLevel = PlayerPrefs.GetInt("current_scene");
        currentLevelText = PlayerPrefs.GetInt("current_scene_text", 0);
        if (LevelText != null)
        {
            LevelText.text = "Level "+ (currentLevelText + 1).ToString();
        }
    }
    public void UpdateTotalCoin()
    {
        SaveTotalCoin(100000);
    }

    public static int GetTotalCoin() => PlayerPrefs.GetInt("LifeTimeScore");
    public static void SaveTotalCoin(int amount) => PlayerPrefs.SetInt("LifeTimeScore", amount);
    public void SetTotalScore()
    {
        int currentLifetimeScore = PlayerPrefs.GetInt("LifeTimeScore", 0);
        int newLifeTimeScore = currentLifetimeScore + RewardValue;
        PlayerPrefs.SetInt("TotalCoinsCount", newLifeTimeScore + TotalCoinsCount);
        PlayerPrefs.SetInt("LifeTimeScore", newLifeTimeScore);
    }

    public void GetTotalScore()
    {
        TotalScore = PlayerPrefs.GetInt("LifeTimeScore");
        PointText.text = TotalScore.ToString("0");
    }
    private void TotalScoreUpdater()
    {
        if (TotalScore < RewardMultiplyValue)
            TotalScore = RewardMultiplyValue;
    }
    public void IncreasePoints(int count)
    {
        /* currentScene = SceneManager.GetActiveScene();*/
        currentLevel = PlayerPrefs.GetInt("current_scene");
        currentLevelText = PlayerPrefs.GetInt("current_scene_text", 0);
        RewardValue += count;
        PointText.text = RewardValue.ToString();

      //  NormalCoin.text = RewardValue.ToString();
      //  RewardMultiplyValue = RewardValue * 2;
        // Multiplied.text = i.ToString();
    }

    private int rewardValueTween = 0;
    private int rewardMultiplyValueTween = 0;



    public void CurrentScoreUpdater()
    {
        rewardValueTween = RewardValue;
        NormalCoin.text = 0.ToString();
        // Multiplied.text = 0.ToString();
        int i = RewardValue;
        RewardValue = GameManager.Instance.WallCollidedWith * i;
        RewardMultiplyValue = 2 * RewardValue;
        DOTween.To(() => rewardValueTween, x => rewardValueTween = x, RewardValue, 2).OnUpdate(UpdateText);
        DOTween.To(() => rewardMultiplyValueTween, x => rewardMultiplyValueTween = x, RewardMultiplyValue, 1)
            .OnUpdate(UpdateText);
    }

    private void UpdateText()
    {
        PointText.text = rewardValueTween.ToString();
        NormalCoin.text = rewardValueTween.ToString();
       // Multiplied.text = rewardMultiplyValueTween.ToString();
    }
    public void NextCallBack()
    {

        UiManager.Instance.UpgradePnael.SetActive(true);

    }
    public void Next()
    {
        DOTween.KillAll();
        UiManager.Instance.SetTotalScore();
 

        if (_currentLevel + 1 >= GameManager.Instance.LevelPrefabs.Count)
        {
            PlayerPrefs.SetInt("current_scene", 0);
            print("reload");

        }
        else
        {
            print("next");
            PlayerPrefs.SetInt("current_scene", _currentLevel + 1);

        }
        PlayerPrefs.SetInt("current_scene_text", currentLevelText + 1);

    }

}

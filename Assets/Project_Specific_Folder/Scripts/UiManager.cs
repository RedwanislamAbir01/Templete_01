using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using TMPro;
using UnityEngine.UI;
public class UiManager : Singleton<UiManager>
{
    public TMP_Text LevelText;
    public GameObject StartUI, EndUi, CompleteUI , FadeIn;
    public GameObject TapFastPanel;

    public GameObject fillbarTimer;
    public Image Timer;
    public float timerInitvalue;
    public override void Start()
    {
        base.Start();

        LevelText.text = SceneLoadCounter.Instance.SceneLoadCount.ToString();
    }


}

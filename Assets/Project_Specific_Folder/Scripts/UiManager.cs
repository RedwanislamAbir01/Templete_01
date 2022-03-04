using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using TMPro;
public class UiManager : Singleton<UiManager>
{
    public TMP_Text LevelText;
    public GameObject StartUI, EndUi, CompleteUI;
    public override void Start()
    {
        base.Start();

        LevelText.text = SceneLoadCounter.Instance.SceneLoadCount.ToString();
    }


}

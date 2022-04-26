using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class GameManager : Singleton<GameManager>
{
    public bool StartGame;
    public PathCreation.Examples.PathFollower p;
    public GameObject FianlCamPos;
    public bool GameOver , GameEnd;
    public AudioSource Fly;
  
    public override void Start()
    {
        p.enabled = false;
        base.Start();
        PlayerPrefs.SetInt("current_scene", SceneManager.GetActiveScene().buildIndex);
    }

    public void Reset()
    {
       
    }

    public void StartIt()
    {
     
     
        UiManager.Instance.StartUI.SetActive(false);
        StartGame = true;
        p.enabled = true;
    }

   
    public void ZoomEffect()
    {
        StartCoroutine(CamZoomInAndOutRoutine());
    }
    public IEnumerator CamZoomInAndOutRoutine()
    {
   
        Camera.main.DOFieldOfView(58, 1); 
        yield return new WaitForSeconds(1);
        Camera.main.DOFieldOfView(70, .5f);
    }
}

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
    public GameObject FianlCamPos, FianlCamPos1;
    public bool GameOver , GameEnd;
    public AudioSource Fly;
    public GameObject BatMobile;
    public override void Start()
    {
        p.enabled = false;
        base.Start();
        PlayerPrefs.SetInt("current_scene", SceneManager.GetActiveScene().buildIndex);
    }

    public void Reset()
    {
        StartCoroutine(ResetRoutine());
    }
    public IEnumerator ResetRoutine()
    {
        FindObjectOfType<Collsion>().Connector.SetActive(false);
        GameManager.Instance.GameOver = true;
        UiManager.Instance.FadeIn.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartIt()
    {
     
        ChracterSetUp();
        UiManager.Instance.StartUI.SetActive(false);
        StartGame = true;
        p.enabled = true;
    }

    private void ChracterSetUp()
    {
        p.GetComponentInChildren<Collsion>().Hero1Model.GetComponent<Animator>().Play("Run"); p.GetComponentInChildren<Collsion>().Hero2Model.GetComponent<Animator>().Play("Run");
        p.GetComponentInChildren<Collsion>().Hero1.GetComponent<LookTowards>().Cape.transform.DOLocalRotate(p.GetComponentInChildren<Collsion>().Hero1.GetComponent<LookTowards>().CapeFinalPos.transform.localEulerAngles, .1f);
        p.GetComponentInChildren<Collsion>().Hero1.GetComponent<LookTowards>().Cape.transform.DOLocalMove(p.GetComponentInChildren<Collsion>().Hero1.GetComponent<LookTowards>().CapeFinalPos.transform.localPosition, .1f);
      //  p.GetComponentInChildren<Collsion>().Hero1Model.GetComponentInChildren<Obi.ObiSolver>().transform.GetChild(0).transform.DOLocalRotate(new Vector3(0, -90, -50), .1f);
      //   p.GetComponentInChildren<Collsion>().Hero1Model.GetComponentInChildren<Obi.ObiSolver>().transform.GetChild(0).transform.DOLocalMove(new Vector3(-2.501f, 2.726f, -0.985f), .1f);
    }
    public void ZoomEffect()
    {
        StartCoroutine(CamZoomInAndOutRoutine());
    }
    public void ZoomEffectCar()
    {
        StartCoroutine(CamZoomInAndOutRoutine1());
    }
    public IEnumerator CamZoomInAndOutRoutine()
    {
   
        Camera.main.DOFieldOfView(58, 1); 
        yield return new WaitForSeconds(1);
        Camera.main.DOFieldOfView(70, .5f);
    }
    public IEnumerator CamZoomInAndOutRoutine1()
    {

        Camera.main.DOFieldOfView(58, 1);
        yield return new WaitForSeconds(1);
        Camera.main.DOFieldOfView(86, .5f);
    }
}

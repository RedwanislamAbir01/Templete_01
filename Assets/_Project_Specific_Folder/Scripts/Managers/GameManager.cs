using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using UnityEngine.SceneManagement;
using DG.Tweening;
using PathCreation.Examples;
public class GameManager : Singleton<GameManager>
{

    [Header("Level prefabs List")]
    public List<GameObject> LevelPrefabs = new List<GameObject>();

    public int levelNo;
    public GameObject currentLvlPrefab;
    GameObject Path;
    public PathCreation.PathCreator pathCreator;
    public PathCreation.Examples.PathFollower p;


    public int WallCollidedWith;
    public bool StartGame;

    public GameObject FianlCamPos;
    public bool GameOver , GameEnd;
    public AudioSource Fly;


    public GameObject BatMobile, Bat;

    public Texture[] RoadTextures;

    int SavedLevelNo;
    public override void Start()
    {
        /*#if UNITY_EDITOR

                levelNo = amarIcchaLevel;
                PlayerPrefs.SetInt("current_scene", levelNo);

        #endif*/
        SavedLevelNo = PlayerPrefs.GetInt("current_scene_text", 0);
        UiManager.Instance.LevelText.text = (SavedLevelNo + 1).ToString();
        int currentLevel = PlayerPrefs.GetInt("current_scene");
        p.enabled = false;
        base.Start();
        LoadLvlPrefab();
        p.GetComponentInChildren<Collsion>().Boss1 = GameObject.FindGameObjectWithTag("Boss");
        BatMobile= GameObject.FindGameObjectWithTag("Batmobil");
    }
    private void Update()
    {
        if (Path == null)
        {
            Path = GameObject.Find("pathWAY");
            pathCreator = Path.GetComponent<PathCreation.PathCreator>();
            Path.GetComponent<RoadMeshCreator>().refresh();
        }
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
        UiManager.Instance.IncreasePoints(0);
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
    public IEnumerator CamZoomInAndOutRoutine()
    {
   
        Camera.main.DOFieldOfView(58, 1); 
        yield return new WaitForSeconds(1);
        Camera.main.DOFieldOfView(70, .5f);
    }
    public void LoadLvlPrefab()
    {

        levelNo = PlayerPrefs.GetInt("current_scene", 0);


        currentLvlPrefab = Instantiate(SuperCop.Scripts.LevelPrefabManager.Instance.GetCurrentLevelPrefab());


    }
}

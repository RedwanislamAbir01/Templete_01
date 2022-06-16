using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using UnityEngine.SceneManagement;
using DG.Tweening;
using PathCreation.Examples;
public class GameManager : Singleton<GameManager>
{
    public bool IsIronManScene;
    public GameObject DC, Marvel;

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


    public GameObject BatMobile,Bike;

    public Texture[] RoadTextures;

    [SerializeField]int SavedLevelNo;
    [SerializeField] int amarIcchaLevel;
    public int BossLevel;

    public bool EnableAmarIccha;
    public SkillTree s;
    public override void Start()
    {
        if (EnableAmarIccha)
        {
#if UNITY_EDITOR

            levelNo = amarIcchaLevel;
            PlayerPrefs.SetInt("current_scene", levelNo);

#endif
        }

        SavedLevelNo = PlayerPrefs.GetInt("current_scene_text", 0);
        UiManager.Instance.LevelText.text = (SavedLevelNo + 1).ToString();
        int currentLevel = PlayerPrefs.GetInt("current_scene");
        p.enabled = false;
        base.Start();
        LoadLvlPrefab();
        if (levelNo >= 0 && levelNo <= 5)
        {
            IsIronManScene = false;
            DC.gameObject.SetActive(true);
        }
       else if (levelNo >= 6)
        {
            if (PlayerPrefs.GetInt("FirstTime", 0) == 0)
            {

                PlayerPrefs.SetInt("FirstTime", 1);
                PlayerPrefs.SetInt("Hero1", 0);
                PlayerPrefs.SetInt("Hero2", 0);

            }
            IsIronManScene = true;

            Marvel.gameObject.SetActive(true); DC.gameObject.SetActive(false);
        }
        p.GetComponentInChildren<Collsion>().Boss1 = GameObject.FindGameObjectWithTag("Boss");
        BatMobile= GameObject.FindGameObjectWithTag("Batmobil");Bike = GameObject.FindGameObjectWithTag("Bike");
        s.enabled = true;
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
    public void EnableTut()
    {
        if (levelNo == 0)
        {
            if (PlayerPrefs.GetInt("Tutorial101", 0) == 0)
            {

                UiManager.Instance.Level1Tut.gameObject.SetActive(true);
                UiManager.Instance.Level1Tut.transform.DOScale(new Vector3(6, 6, 6), .1f).OnComplete(() => { Time.timeScale = 0; });


                PlayerPrefs.SetInt("Tutorial101", 1);


            }
        }
        if(levelNo == 6)
            if (PlayerPrefs.GetInt("Tutorial106", 0) == 0)
            {


                UiManager.Instance.Level6Tut.gameObject.SetActive(true);
                UiManager.Instance.Level6Tut.transform.DOScale(new Vector3(6, 6, 6), .1f).OnComplete(() => { Time.timeScale = 0; });


                PlayerPrefs.SetInt("Tutorial106", 1);


            }
    
    }
    public void Reset()
    {
        StartCoroutine(ResetRoutine());
    }
    public IEnumerator ResetRoutine()
    {
     
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

        Invoke("EnableTut", 1f);
    }

    private void ChracterSetUp()
    {
        p.GetComponentInChildren<Collsion>().Hero1Model.GetComponent<Animator>().Play("Run"); p.GetComponentInChildren<Collsion>().Hero2Model.GetComponent<Animator>().Play("Run");
        p.GetComponentInChildren<Collsion>().Hero1.GetComponent<PerCollsion>().Cape.transform.DOLocalRotate(p.GetComponentInChildren<Collsion>().Hero1.GetComponent<PerCollsion>().CapeFinalPos.transform.localEulerAngles, .1f);
        p.GetComponentInChildren<Collsion>().Hero1.GetComponent<PerCollsion>().Cape.transform.DOLocalMove(p.GetComponentInChildren<Collsion>().Hero1.GetComponent<PerCollsion>().CapeFinalPos.transform.localPosition, .1f);
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

        BossLevel = currentLvlPrefab.GetComponent<LevelDetails>().BossLevel;
    }
}

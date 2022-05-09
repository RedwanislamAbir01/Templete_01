using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Collsion : MonoBehaviour
{
    public Text LevelText , ColorText;
    public Animator anim , anim1;

    public GameObject SecondHand;
    public Texture[] Tattos , CheapTttos;
    public Texture[] BadBlue, GoodBlue, GoodYellow, BadYellow;
    public Material StiackerMat;
    public int min = 0, max = 255;
    public Texture Default;
   [SerializeField] bool IsGoodGate;
    Vector3 Startpos;
    public GameObject Boss;

    public bool StartTapRoutine;
    private void Start()
    {
        StiackerMat.mainTexture = Default;
        StiackerMat.DOFade(0, 0);
        Startpos = transform.localPosition;

    }
    private void Update()
    {
        if (StartTapRoutine)
        {
            if(GameManager.Instance.PivotParent.transform.eulerAngles.x <= 15)
            {
                StartTapRoutine = false;
                UiManager.Instance.TapFastPanel.gameObject.SetActive(false);
                UiManager.Instance.CompleteUI.gameObject.SetActive(true); 
                  GameManager.Instance.PivotParent.transform.DOLocalRotate(new Vector3(27.824f, 0, 0), .5f);
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (UiManager.Instance.timerInitvalue < 1f)
                {
                    UiManager.Instance.timerInitvalue += 0.12f;
                    
                    UiManager.Instance.Timer.fillAmount = UiManager.Instance.timerInitvalue;
                    
                    UiManager.Instance.Timer.fillAmount = UiManager.Instance.timerInitvalue;
                    GameManager.Instance.PivotParent.GetComponent<MySDK.Rotator>().enabled = false;
                    GameManager.Instance.PivotParent.transform.DOLocalRotate(new Vector3(GameManager.Instance.PivotParent.transform.eulerAngles.x + UiManager.Instance.timerInitvalue, 0, 0), .1f);

                    Camera.main.transform.DOShakePosition(1.5f, .01f);
                    Camera.main.DOFieldOfView(60, 2);
                }

            }

            if (UiManager.Instance.timerInitvalue > 0f)
            {
                UiManager.Instance.timerInitvalue -= 0.0075f;
                UiManager.Instance.Timer.fillAmount = UiManager.Instance.timerInitvalue;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("GoodGate"))
        {;
          
            StartCoroutine(AnimationDelayRoutine());
            GameManager.Instance.Level++;
            Invoke("UpdateTexture", .2f);
            IsGoodGate = true;
            other.GetComponent<BoxCollider>().enabled = false;
        }
        if (other.gameObject.CompareTag("BadGate"))
        {
           
            StartCoroutine(AnimationDelayRoutine());
            GameManager.Instance.Level++;
            Invoke("UpdateTextureCheap", .2f);
            IsGoodGate = false;
            LevelText.text = "Bad :" + GameManager.Instance.Level.ToString(); ColorText.text = "Red";
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(SpeedSlowDownRoutine());
            StartCoroutine(UiManager.Instance.FdeDelayRoutine());
            StiackerMat.DOFade(0, .3f); anim1.Play("Hurt"); anim.Play("Hurt");
            other.GetComponent<BoxCollider>().enabled = false;
        }
        if (other.gameObject.CompareTag("Yellow"))
        {
            StartCoroutine(AnimationDelayRoutine());
            if (IsGoodGate)
            {
                if(GameManager.Instance.Level == 4)
                {
                    StiackerMat.DOFade(0, .3f).OnComplete(() =>
                    {
                        StiackerMat.mainTexture = GoodYellow[0];
                        StiackerMat.DOFade(1, .5f);
                    });
                }
                else if (GameManager.Instance.Level == 5)
                {
                    StiackerMat.DOFade(0, .3f).OnComplete(() =>
                    {
                        StiackerMat.mainTexture = GoodYellow[01];
                        StiackerMat.DOFade(1, .5f);
                    });
                }
            }
            else
            {
                if (GameManager.Instance.Level == 4)
                {
                    StiackerMat.DOFade(0, .3f).OnComplete(() =>
                    {
                        StiackerMat.mainTexture = BadYellow[0];
                        StiackerMat.DOFade(1, .5f);
                    });
                }
                else  if (GameManager.Instance.Level == 5)
                {
                    StiackerMat.DOFade(0, .3f).OnComplete(() =>
                    {
                        StiackerMat.mainTexture = BadYellow[01];
                        StiackerMat.DOFade(1, .5f);
                    });
                }
            }
        }
        if (other.gameObject.CompareTag("Blue"))
        {
            StartCoroutine(AnimationDelayRoutine());
            if (IsGoodGate)
            {
                if (GameManager.Instance.Level == 4)
                {
                    StiackerMat.DOFade(0, .3f).OnComplete(() =>
                    {
                        StiackerMat.mainTexture = GoodBlue[0];
                        StiackerMat.DOFade(1, .5f);
                    });
                }
             else   if (GameManager.Instance.Level == 5)
                {
                    StiackerMat.DOFade(0, .3f).OnComplete(() =>
                    {
                        StiackerMat.mainTexture = GoodBlue[01];
                        StiackerMat.DOFade(1, .5f);
                    });
                }
            }
            else
            {
                if (GameManager.Instance.Level == 4)
                {
                    StiackerMat.DOFade(0, .3f).OnComplete(() =>
                    {
                        StiackerMat.mainTexture = BadBlue[0];
                        StiackerMat.DOFade(1, .5f);
                    });
                }
               else if (GameManager.Instance.Level == 5)
                {
                    StiackerMat.DOFade(0, .3f).OnComplete(() =>
                    {
                        StiackerMat.mainTexture = BadBlue[01];
                        StiackerMat.DOFade(1, .5f);
                    });
                }
            }
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            StartCoroutine(StopRoutine(other.gameObject));
        }
    }
    public IEnumerator AnimationDelayRoutine()
    {
        yield return new WaitForSeconds(.55f);
        RandomAnimationPlay();
    }
    public IEnumerator SpeedSlowDownRoutine()
    {
        GameManager.Instance.p.speed = GameManager.Instance.p.MaxSpeed = .8f;
        yield return new WaitForSeconds(.6f);
        GameManager.Instance.p.MaxSpeed = 2;
    }

    void RandomAnimationPlay() {
        int i = Random.Range(0, 3);
        if(i== 0)
        {
            anim.Play("g 0"); anim1.Play("g 0");
        }
        if (i == 1)
        {
            StartCoroutine(GoodGateRot());
            anim.Play("g 1"); anim1.Play("g 1");
        }
        if(i == 2)
        {
            anim.Play("g"); anim1.Play("g");
        }
    }
    public IEnumerator StopRoutine(GameObject g )

    {
        //Camera.main.transform.parent = g.transform.root;
      
        transform.GetComponent<Controller>().enabled = false; anim1.transform.GetComponent<Controller>().enabled = false;
        anim.transform.DOLocalMoveX(-1.66f, .1f); anim1.transform.DOLocalMoveX(-1.66f, .1f);
      

        transform.root.parent = g.transform.root;
     
        Camera.main.transform.DOLocalMove(GameManager.Instance.FianlCamPos.transform.localPosition, 1);
        Camera.main.transform.DOLocalRotate(GameManager.Instance.FianlCamPos.transform.localEulerAngles, 1);
        yield return new WaitForSeconds(1.4f);
        transform.DOLocalRotate(new Vector3(0, -90, 9), .1f); anim1.transform.DOLocalRotate(new Vector3(0, -90, 9), .1f);
        Boss.transform.GetComponent<Animator>().enabled = true;
        GameManager.Instance.p.MaxSpeed = .3f;
        GameManager.Instance.p.speed = .3f;
        anim.Play("Wrestle"); anim1.Play("Wrestle");
        Camera.main.transform.parent = g.transform.root;
        transform.parent.parent = GameManager.Instance.PivotParent.transform; Boss.transform.parent = GameManager.Instance.PivotParent.transform;
        yield return new WaitForSeconds(.2f);
        GameManager.Instance.PivotParent.transform.GetComponent<MySDK.Rotator>().enabled = true;
        this.transform.root.DOLocalMove(new Vector3(-0.3102503f, 0, -0.205f), .3f);
        GameManager.Instance.p.enabled = false;
        StartTapRoutine = true;
        UiManager.Instance.TapFastPanel.SetActive(true);

    }

    public void ChangeMaterials()
    {
        StiackerMat.DOFade(1, 1.8f);
    }

    public void UpdateTexture()
    {
        
            StiackerMat.DOFade(0, .3f).OnComplete(() =>
            {
                StiackerMat.mainTexture = Tattos[GameManager.Instance.Level - 1];
                StiackerMat.DOFade(1, .5f);
            });
        
    }
    public IEnumerator GoodGateRot()
    {
        ///GetComponent<Controller>().enabled = false;
        transform.DOLocalMove(new Vector3(-1.35f, 3.15f, -0.67f), .1f); anim1. transform.DOLocalMove(new Vector3(-1.35f, 3.15f, -0.67f), .1f);
        yield return new WaitForSeconds(.2f);
        //GetComponent<Controller>().enabled = true;
        transform.DOLocalMove(Startpos, .1f);
        anim1.transform.DOLocalMove(Startpos, .1f);

    }
    public void UpdateTextureCheap()
    {
        StiackerMat.DOFade(0, .3f).OnComplete(() => {
            StiackerMat.mainTexture = CheapTttos[GameManager.Instance.Level - 1];
            StiackerMat.DOFade(1, .5f);
        });
    }
}

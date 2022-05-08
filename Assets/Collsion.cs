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
    private void Start()
    {
        StiackerMat.mainTexture = Default;
        StiackerMat.DOFade(0, 0);

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
            StartCoroutine(StopRoutine());
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
            anim.Play("g 1"); anim1.Play("g 1");
        }
        if(i == 2)
        {
            anim.Play("g"); anim1.Play("g");
        }
    }
    public IEnumerator StopRoutine()
    {
        Camera.main.transform.DOLocalMove(GameManager.Instance.FianlCamPos.transform.localPosition, 1);
        Camera.main.transform.DOLocalRotate(GameManager.Instance.FianlCamPos.transform.localEulerAngles, 1);
        yield return new WaitForSeconds(1.4f);
        GameManager.Instance.p.MaxSpeed = .3f;
        GameManager.Instance.p.speed = .3f;

        yield return new WaitForSeconds(.2f); anim.Play("Wrestle"); anim1.Play("Wrestle");
        this.transform.root.DOMove(new Vector3(8.404f, 0, -0.052f), .3f);
        GameManager.Instance.p.enabled = false;


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
    public void UpdateTextureCheap()
    {
        StiackerMat.DOFade(0, .3f).OnComplete(() => {
            StiackerMat.mainTexture = CheapTttos[GameManager.Instance.Level - 1];
            StiackerMat.DOFade(1, .5f);
        });
    }
}

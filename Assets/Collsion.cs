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
    public Texture[] Tattos;
    public Material StiackerMat;
    public int min = 0, max = 255;
    public Texture Default;
    private void Start()
    {
        StiackerMat.mainTexture = Default;
        StiackerMat.DOFade(0, 0);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("GoodGate"))
        {

            StartCoroutine(AnimationDelayRoutine());
            GameManager.Instance.Level++; 
            UpdateTexture();
            LevelText.text = "good :" + GameManager.Instance.Level.ToString() ;
            ColorText.text = "green";
        }
        if (other.gameObject.CompareTag("BadGate"))
        {
            StartCoroutine(AnimationDelayRoutine());
            GameManager.Instance.Level++;
            LevelText.text = "Bad :" + GameManager.Instance.Level.ToString(); ColorText.text = "Red";
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(SpeedSlowDownRoutine());
            GameManager.Instance.Level--; 
            StiackerMat.DOFade(0, .3f);
            LevelText.text = "No sticker"; ColorText.text = "No color";
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

    public void ChangeMaterials()
    {
        StiackerMat.DOFade(1, 1.8f);
    }

    public void UpdateTexture()
    {
        StiackerMat.DOFade(0, .3f).OnComplete(() => {
            StiackerMat.mainTexture = Tattos[GameManager.Instance.Level];
            StiackerMat.DOFade(1, .5f);
        });
    }
}

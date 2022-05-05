using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collsion : MonoBehaviour
{
    public Text LevelText , ColorText;
    public Animator anim;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("GoodGate"))
        {
            RandomAnimationPlay();
            GameManager.Instance.Level++;
            LevelText.text = "good :" + GameManager.Instance.Level.ToString() ;
            ColorText.text = "green";
        }
        if (other.gameObject.CompareTag("BadGate"))
        {
            RandomAnimationPlay();
            GameManager.Instance.Level++;
            LevelText.text = "Bad :" + GameManager.Instance.Level.ToString(); ColorText.text = "Red";
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(SpeedSlowDownRoutine());
            GameManager.Instance.Level--;
            LevelText.text = "No sticker"; ColorText.text = "No color";
        }
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
            anim.Play("g 0");
        }
        if (i == 1)
        {
            anim.Play("g 1");
        }
        if(i == 2)
        {
            anim.Play("g");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hammer : MonoBehaviour
{
    public EndDetector E;
    Vector3 StartPos;
    public GameObject Hammers;
    public TrailRenderer t;
    public ParticleSystem Electric;
    private void Start()
    {
        t.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            if (PlayerPrefs.GetInt("Hero1") != 2)
            {
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.ShieldGuy && GetComponentInParent<PerCollsion>().Type == eType.Hero1)
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    Hammers.gameObject.SetActive(false);
                    SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                    E.Anim.SetTrigger("Punch");
                    GetComponent<BoxCollider>().enabled = false;

                    t.enabled = true;
                    transform.DOLocalMoveZ(5, .3f).OnComplete(() =>
                    {
                        other.transform.GetChild(1).gameObject.SetActive(true);
                        other.GetComponentInChildren<Animator>().Play("Death");
                        BackToOld();
                    });
                    print("---");

                    other.GetComponent<Collider>().enabled = false;


                }
                else if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.KryptoCrstalguy&& GetComponentInParent<PerCollsion>().Type == eType.Hero1)
                {

                    transform.GetChild(0).gameObject.SetActive(true);
                    Hammers.gameObject.SetActive(false);
                    SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                    E.Anim.SetTrigger("Punch");
                    GetComponent<BoxCollider>().enabled = false;

                    t.enabled = true;
                    transform.DOLocalMoveZ(5, .3f).OnComplete(() =>
                    {
                        other.transform.GetChild(1).gameObject.SetActive(true);
                        
                        other.GetComponentInChildren<Animator>().Play("Taunt");
                        BackToOld();
                    });



                }
                else if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Rock && GetComponentInParent<PerCollsion>().Type == eType.Hero1)
                {

                    transform.GetChild(0).gameObject.SetActive(true);
                    Hammers.gameObject.SetActive(false);
                    SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                    E.Anim.SetTrigger("Punch");
                    GetComponent<BoxCollider>().enabled = false;

                    t.enabled = true;
                    transform.DOLocalMoveZ(5, .3f).OnComplete(() =>
                    {

                        BackToOld();
                    });



                }
            }
            else  if (PlayerPrefs.GetInt("Hero1") == 2)

            {
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.ShieldGuy && GetComponentInParent<PerCollsion>().Type == eType.Hero1)
                {
                    other.GetComponent<Collider>().enabled = false;
                    Electric.Play();
                    other.transform.GetChild(1).gameObject.SetActive(true);
                    other.GetComponentInChildren<Animator>().Play("Death");

                }
                else if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Rock && GetComponentInParent<PerCollsion>().Type == eType.Hero1)
                {

                    Electric.Play();
                   
                        other.gameObject.GetComponent<Collider>().enabled = false;
                        other.transform.GetChild(0).gameObject.SetActive(true); other.transform.GetChild(01).gameObject.SetActive(false);
                   



                }
                else
                {
                    Electric.Play(); other.GetComponentInChildren<Animator>().Play("Death");
                }
            }
        }
    }
    public void BackToOld()
    {

        GetComponent<BoxCollider>().enabled = true;
        transform.DOLocalMove(StartPos, .4f).OnComplete(() => {
            transform.GetChild(0).gameObject.SetActive(false);
            Hammers.gameObject.SetActive(true);
        });
    }
}

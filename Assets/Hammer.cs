using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;
public class Hammer : MonoBehaviour
{
    public EndDetector E;
    Vector3 StartPos;
    public GameObject Hammers;

    public ParticleSystem Electric;
    private void Start()
    {
        StartPos = transform.localPosition;
   
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.GameOver)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                if (PlayerPrefs.GetInt("Hero1") != 2)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.ShieldGuy && GetComponentInParent<PerCollsion>().Type == eType.Hero1)
                    {
                        transform.GetChild(0).gameObject.SetActive(true);
                        Hammers.gameObject.SetActive(false);
                        SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                        E.Anim.SetTrigger("Punch");
                        GetComponent<BoxCollider>().enabled = false;


                        transform.DOLocalMove(new Vector3(0.282f, 1.09f, 8f), .3f).OnComplete(() =>
                     {
                         other.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
                         other.GetComponentInChildren<Animator>().Play("Death");

                         BackToOld();
                     });
                        print("---");

                        other.GetComponent<Collider>().enabled = false;


                    }
                    else if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.KryptoCrstalguy && GetComponentInParent<PerCollsion>().Type == eType.Hero1)
                    {
                        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);

                        transform.GetChild(0).gameObject.SetActive(true);
                        Hammers.gameObject.SetActive(false);
                        SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                        E.Anim.SetTrigger("Punch");
                        GetComponent<BoxCollider>().enabled = false;


                        transform.DOLocalMoveZ(8, .3f).OnComplete(() =>
                        {
                            other.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
         
                            other.GetComponentInChildren<Animator>().Play("Taunt");
                            BackToOld();
                        });



                    }
                    else if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Rock && GetComponentInParent<PerCollsion>().Type == eType.Hero1)
                    {
                        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);

                        transform.GetChild(0).gameObject.SetActive(true);
                        Hammers.gameObject.SetActive(false);
                        SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                        E.Anim.SetTrigger("Punch");
                        GetComponent<BoxCollider>().enabled = false;


                        transform.DOLocalMoveZ(8, .3f).OnComplete(() =>
                        {
                            other.gameObject.GetComponent<Collider>().enabled = false;
                            other.transform.GetChild(0).gameObject.SetActive(true);
                            other.transform.GetChild(1).gameObject.SetActive(false);
                            BackToOld();
                        });



                    }
                }
                else if (PlayerPrefs.GetInt("Hero1") == 2)

                {
                    if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.ShieldGuy && GetComponentInParent<PerCollsion>().Type == eType.Hero1)
                    {
                        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                        other.GetComponent<Collider>().enabled = false;
                        Electric.Play();
                        other.transform.GetChild(1).gameObject.SetActive(true);
           
                        other.GetComponentInChildren<Animator>().Play("Death");

                    }
                    else if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Rock && GetComponentInParent<PerCollsion>().Type == eType.Hero1)
                    {
                        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                        Electric.Play();

                        other.gameObject.GetComponent<Collider>().enabled = false;
                        other.transform.GetChild(0).gameObject.SetActive(true);
                        other.transform.GetChild(1).gameObject.SetActive(false);




                    }
                    else
                    {
                        if (other.gameObject.GetComponent<Enemy>().EnemyType != eEnemyType.ElectricWall && GetComponentInParent<PerCollsion>().Type == eType.Hero1)
                        {
                            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                            other.gameObject.GetComponent<Collider>().enabled = false;

                            Electric.Play();
                            if (other.GetComponentInChildren<Animator>() != null)
                                other.GetComponentInChildren<Animator>().Play("Death");
                        }
                    }
                }
            }
        }
    }
    public void BackToOld()
    {

      
        transform.DOLocalMove(StartPos, .6f).OnComplete(() => {
            transform.GetChild(0).gameObject.SetActive(false); 
            Hammers.gameObject.SetActive(true);
            Invoke("DisableCollider", 1.2f);
        });
    }

    public void DisableCollider()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;
public class MoveForward : MonoBehaviour
{
    public EndDetector E;
    public ShieldDetector S;
    Vector3 StartPos;
    public GameObject Hammer;

    public bool RageMode;
    void Start()
    {
        StartPos = transform.localPosition;
     
    }

    // Update is called once per frame
    void Update()
    {
        if (S != null)
        {
            if (S.Enable)
            {
                transform.Translate(Vector3.forward * 5 * Time.deltaTime);
            }
        }
    }
    public IEnumerator PauseGame(float pauseTime)
    {
        Debug.Log("Inside PauseGame()");
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
        Debug.Log("Done with my pause");
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {


            if (GameManager.Instance.IsHulkScene)
            {
                if (!RageMode)
                {
                    if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.KryptoCrstalguy && GetComponentInParent<PerCollsion>().Type == eType.Hero2)
                    {
                        if (PlayerPrefs.GetInt("Hero2") == 2)
                        {
                            SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                            GetComponent<BoxCollider>().enabled = false;
                            E.Anim.SetTrigger("Punch");

                            other.GetComponent<Collider>().enabled = false;

                            SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().TankHit);
                            other.GetComponent<Collider>().enabled = false;

                            other.GetComponentInChildren<Animator>().Play("Death");

                            other.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
                        }
                        else
                        {
                            SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                            GetComponent<BoxCollider>().enabled = false;
                            E.Anim.SetTrigger("Punch");

                            other.GetComponent<Collider>().enabled = false;

                            SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().TankHit);
                            other.GetComponent<Collider>().enabled = false;

                            other.GetComponentInChildren<Animator>().Play("Death");

                            other.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
                        }
                    }
                    else if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.ShieldGuy && GetComponentInParent<PerCollsion>().Type == eType.Hero2)
                    {
                        if (PlayerPrefs.GetInt("Hero2") == 2)
                        {
                       

                            SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                            E.Anim.SetTrigger("Punch");


                            other.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
                            other.GetComponentInChildren<Animator>().Play("Death");




                            other.GetComponent<Collider>().enabled = false;
                        }
                    }

                }

                else
                {
                    
                      
                    
                     
                    if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.KryptoCrstalguy )
                    {
                        
                            SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                         
                            E.Anim.SetTrigger("Punch");

                            other.GetComponent<Collider>().enabled = false;

                            SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().TankHit);
                            other.GetComponent<Collider>().enabled = false;

                            other.GetComponentInChildren<Animator>().Play("Death");

                        other.transform.GetChild(1).gameObject.SetActive(true);
                        StartCoroutine(PauseGame(.1f));
                    }
                    if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.ShieldGuy )
                    {
                        
                            print("aa");

                            SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                            E.Anim.SetTrigger("Punch");


                        other.transform.GetChild(1).gameObject.SetActive(true);

                        other.GetComponentInChildren<Animator>().Play("Death");




                            other.GetComponent<Collider>().enabled = false;
                        StartCoroutine(PauseGame(.1f));
                    }
                    else
                    {
                        StartCoroutine(PauseGame(.1f));
                    }
                }
            }
            else
            {
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.ShieldGuy && GetComponentInParent<PerCollsion>().Type == eType.Hero1)
                {
                    SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                    MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                    GetComponent<BoxCollider>().enabled = false;
                    E.Anim.SetTrigger("Punch");
                    Invoke("BackToOld", .5f);
                    other.GetComponent<Collider>().enabled = false;
                    other.transform.GetChild(2).gameObject.SetActive(true);
                    other.GetComponentInChildren<Animator>().Play("Death");
                    S.Enable = false;
                }
            }
        }
    }

  public  void BackToOld()
    {
        GetComponent<BoxCollider>().enabled = true;
        transform.DOLocalMove(StartPos, .3f);
    }
}

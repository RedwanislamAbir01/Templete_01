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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
          

            if (GameManager.Instance.IsHulkScene)
            {
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.KryptoCrstalguy)
                {
                    SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                    MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                    GetComponent<BoxCollider>().enabled = false;
                    E.Anim.SetTrigger("Punch");
                   
                    other.GetComponent<Collider>().enabled = false;
                    
                        SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().TankHit);
                    other.GetComponent<Collider>().enabled = false;
                    
                    other.GetComponentInChildren<Animator>().Play("Death");

                    other.transform.GetChild(1).gameObject.SetActive(true);
                }
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.ShieldGuy && GetComponentInParent<PerCollsion>().Type == eType.Hero1)
                {
                    SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                    MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                    GetComponent<BoxCollider>().enabled = false;
                    // E.Anim.SetTrigger("Punch");
                    Hammer.transform.DOLocalMoveZ(5, .5f);
                    print("---");
                    Invoke("BackToOld", .5f);
                    other.GetComponent<Collider>().enabled = false;
                    other.transform.GetChild(1).gameObject.SetActive(true);
                    other.GetComponentInChildren<Animator>().Play("Death");
                
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

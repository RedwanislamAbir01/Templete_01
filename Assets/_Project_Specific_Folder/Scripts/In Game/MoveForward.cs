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
    void Start()
    {
        StartPos = transform.localPosition;
     
    }

    // Update is called once per frame
    void Update()
    {
        if (S.Enable)
        {
            transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.ShieldGuy)
            {
                SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                GetComponent<BoxCollider>().enabled = false;
                 E.Anim.SetTrigger("Punch");
                Invoke("BackToOld" , .5f);
                other.GetComponent<Collider>().enabled = false;
                other.transform.GetChild(2).gameObject.SetActive(true);
                other.GetComponentInChildren<Animator>().Play("Death");
                S.Enable = false;
            }
        }
    }

  public  void BackToOld()
    {
        GetComponent<BoxCollider>().enabled = true;
        transform.DOLocalMove(StartPos, .3f);
    }
}

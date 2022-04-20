using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;
public class EndDetector : MonoBehaviour
{
    public Animator Anim;
    public Transform SpawnPoint;
    LookTowards lf;
    public GameObject Projectile , Laser;
    public float LaserTime;
    public GameObject Puncher;
    public bool Enable;

    private void Start()
    {
        lf = GetComponentInParent<LookTowards>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blue"))
        {
          //  Camera.main.transform.DOLocalRotate(GameManager.Instance.FianlCamPos.transform.localEulerAngles, 1.5f);
         //   Camera.main.transform.DOLocalMove(GameManager.Instance.FianlCamPos.transform.localPosition, 1.5f);
        }


        if (other.gameObject.CompareTag("Enemy"))
        {
            if(other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Wall || other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.KyptoBlock)
            {
                if(lf.Type == eType.Hero2)
                {
                    Anim.SetTrigger("Throw");
                    Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
                }
                else
                {
                    StartCoroutine(LaserEnableDisableRoutine());
                }

            }
            if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.KryptoCrstalguy)
            {
                if (lf.Type == eType.Hero2)
                {
                    Anim.SetTrigger("Throw");
                    Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
                }
                else
                {
                    StartCoroutine(LaserEnableDisableRoutine());
                    //Puncher.GetComponent<Animator>().enabled = false;
                    //Puncher.transform.DOLocalMoveZ(30, .3f).SetEase(Ease.InSine).OnComplete(() => {

                    //    Puncher.transform.DOLocalMoveZ(0, .2f).OnComplete(() => {
                    //        Puncher.GetComponent<Animator>().enabled = true;

                    //    });
                    //});
                }

            }
            if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.BrickWall)
            {
                if (lf.Type == eType.Hero1)
                {
                   // Anim.SetTrigger("Throw");
                    Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
                }
            }
            if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.LaserWall)
            {
                if (lf.Type == eType.Hero2)
                {
                     Anim.SetTrigger("Throw");
                    Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
                }
            }
            if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Ice)
            {
                if (lf.Type == eType.Hero2)
                {
                    Anim.SetTrigger("Throw");
                    Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
                }
                else
                {
                    MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                    other.GetComponent<Collider>().enabled = false;
                    other.transform.GetComponent<Enemy>().WaterSpill.transform.DOScale(new Vector3(0.227319f, 0.227319f, 0.5852864f), 1.5f);
                    StartCoroutine(LaserEnableDisableRoutine());
                    other.transform.root.transform.DOScaleY(0, 1.5f).OnComplete(() => {
                        Destroy(other.gameObject);

                    });
                }

            }
            if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.ShieldGuy)
            {
                if (lf.Type == eType.Hero2)
                {
                    Anim.SetTrigger("Throw");
                    Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
                }
                else
                {

                    Enable = true;
                   
                    //Anim.SetTrigger("Punch");
                    //Puncher.GetComponent<Animator>().enabled = false;
                    //Puncher.transform.DOLocalMoveZ(25, .6f).SetEase(Ease.Linear).OnComplete(() => {

                    //    other.GetComponent<Collider>().enabled = false;
                    //    other.GetComponentInChildren<Animator>().Play("Death");
                    //    Puncher.transform.DOLocalMoveZ(0, .4f).SetEase(Ease.Linear).OnComplete(() => {
                    //        Puncher.GetComponent<Animator>().enabled = true;

                    //    });
                    // });
                }

            }

        }
    }
    public IEnumerator LaserEnableDisableRoutine()
    {
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        Laser.gameObject.SetActive(true);
        yield return new WaitForSeconds(LaserTime);
        Laser.gameObject.SetActive(false);
            
    }
}

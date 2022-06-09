using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;
public class EndDetector : MonoBehaviour
{
    public Animator Anim;
    public Transform SpawnPoint, SpwanPoint1;
    public GameObject aa, bb;


    LookTowards lf;
    public GameObject Projectile, Laser;
    public float LaserTime;
    public GameObject Puncher;
    public bool Enable;
    Vector3 StartPos;
    public ParticleSystem a, b;
    public bool IsBatMobile;

    private void Start()
    {
        if (Anim != null)
            StartPos = Anim.transform.position;
        lf = GetComponentInParent<LookTowards>();
    }
    public IEnumerator SpriteOnOff()
    {
        aa.gameObject.SetActive(true); bb.gameObject.SetActive(true);
        yield return new WaitForSeconds(.1f);
        aa.gameObject.SetActive(false); bb.gameObject.SetActive(false);
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
            if (!IsBatMobile)
            {
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Wall || other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.KyptoBlock)
                {
                    if (lf.Type == eType.Hero2)
                    {
                        SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().BatBlade);
                        Anim.SetTrigger("Throw");
                        StartCoroutine(LevelTwoBatThrowRoutine());
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
                        SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().BatBlade);
                        Anim.SetTrigger("Throw"); StartCoroutine(LevelTwoBatThrowRoutine());
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
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.BrickWall || other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Lizard)
                {
                    if (lf.Type == eType.Hero1)
                    {
                        Anim.SetTrigger("Shoot"); SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().IronShoot);
                        Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
                    }
                    else
                    {
                        SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().Webshoot);
                        Anim.SetTrigger("Throw"); StartCoroutine(LevelTwoBatThrowRoutine());
                        Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
                    }
                }
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.LaserWall)
                {
                    if (lf.Type == eType.Hero2)
                    {
                        SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().Webshoot);
                        Anim.SetTrigger("Throw"); StartCoroutine(LevelTwoBatThrowRoutine());
                        Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
                    }
                    else
                    {
                        SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().IronShoot);
                        Anim.SetTrigger("Shoot");
                        Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
                    }
                }
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.WarMachine)
                {
                    if (lf.Type == eType.Hero2)
                    {
                        SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().Webshoot);
                        Anim.SetTrigger("Throw"); StartCoroutine(LevelTwoBatThrowRoutine());
                        Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
                    }
                    else
                    {
                        SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().IronShoot);
                        Anim.SetTrigger("Shoot");
                        Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
                    }
                }
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Ice)
                {
                    if (lf.Type == eType.Hero2)
                    {
                        SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().BatBlade);
                        Anim.SetTrigger("Throw"); StartCoroutine(LevelTwoBatThrowRoutine());
                        Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
                    }
                    else
                    {
                        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                        other.GetComponent<Collider>().enabled = false;
                        other.transform.GetComponent<Enemy>().WaterSpill.transform.DOScale(new Vector3(0.227319f, 0.227319f, 0.5852864f), 1.5f);
                        StartCoroutine(LaserEnableDisableRoutine());
                        other.transform.parent.transform.DOScaleY(0, 1.5f).OnComplete(() =>
                        {
                            Destroy(other.gameObject);

                        });
                    }

                }
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.ShieldGuy)
                {
                    if (lf.Type == eType.Hero2)
                    {
                        Anim.SetTrigger("Throw"); StartCoroutine(LevelTwoBatThrowRoutine());
                        Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
                    }
                    else
                    {

                        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                        other.GetComponent<Collider>().enabled = false;
                       
                        StartCoroutine(LaserEnableDisableRoutine());
                        StartCoroutine(EnemyDeath(other.gameObject));
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
            else
            {
                StartCoroutine(Shoot6Times());
            }
        }
    }
    public IEnumerator EnemyDeath(GameObject g)
    {
        yield return new WaitForSeconds(.3f);
        g.GetComponentInChildren<Animator>().Play("Death");
    }
    public IEnumerator LaserEnableDisableRoutine()
    {
        //SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().SuperLaser);
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        Laser.gameObject.SetActive(true);
        yield return new WaitForSeconds(LaserTime);
        Laser.gameObject.SetActive(false);

    }
    public IEnumerator LevelTwoBatThrowRoutine()
    {
        if(PlayerPrefs.GetInt("Batman") == 2)
        {
            lf.Power2.gameObject.SetActive(true);
            yield return new WaitForSeconds(.3f);
            lf.Power2.gameObject.SetActive(false);
        }
    }
    public IEnumerator Shoot6Times()
    {





        GameObject g = Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
        g.transform.DOLocalRotate(new Vector3(0, 1, 0), 0);
        GameObject g1 = Instantiate(Projectile, SpwanPoint1.position, Quaternion.identity); g1.GetComponent<Collider>().enabled = false;
        g1.transform.DOLocalRotate(new Vector3(0, -1, 0), 0);
        StartCoroutine(SpriteOnOff());
        yield return new WaitForSeconds(.2f);
        GameObject g2 = Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
        g2.GetComponent<Collider>().enabled = false;
        g2.transform.DOLocalRotate(new Vector3(0, 1, 0), 0);
        GameObject g3 = Instantiate(Projectile, SpwanPoint1.position, Quaternion.identity);
        g3.GetComponent<Collider>().enabled = false;
        g3.transform.DOLocalRotate(new Vector3(0, -1, 0), 0);
        StartCoroutine(SpriteOnOff());
        yield return new WaitForSeconds(.2f);
        GameObject g4 = Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
        g4.GetComponent<Collider>().enabled = false;
        g4.transform.DOLocalRotate(new Vector3(0, 1, 0), 0);
        GameObject g5 = Instantiate(Projectile, SpwanPoint1.position, Quaternion.identity);
        g5.GetComponent<Collider>().enabled = false;
        g5.transform.DOLocalRotate(new Vector3(0, -1, 0), 0);
        StartCoroutine(SpriteOnOff());

    }
}

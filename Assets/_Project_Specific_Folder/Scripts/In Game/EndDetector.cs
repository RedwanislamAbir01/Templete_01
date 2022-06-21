using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;
public class EndDetector : MonoBehaviour
{
    public Animator Anim;
    public Transform SpawnPoint, SpwanPoint1, NewGunSpawnPoint;
    public GameObject aa, bb;
    public ParticleSystem poof;

    PerCollsion lf;
    public GameObject Projectile, Laser , SpiderNet;
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
        lf = GetComponentInParent<PerCollsion>();
    }
    public IEnumerator SpriteOnOff()
    {
        if (aa != null && bb != null)
        {
            aa.gameObject.SetActive(true); bb.gameObject.SetActive(true);
            yield return new WaitForSeconds(.1f);
            aa.gameObject.SetActive(false); bb.gameObject.SetActive(false);
        }
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
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Wall)
                {
                    if (lf.Type == eType.Hero2)
                    {
                       
                        Anim.SetTrigger("Throw");
                        StartCoroutine(LevelTwoBatThrowRoutine());
                        Shoot();
                    }
                    else
                    {
                        StartCoroutine(LaserEnableDisableRoutine());
                    }

                }
                if ( other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.KyptoBlock)
                {
                    if (lf.Type == eType.Hero2)
                    {

                        Anim.SetTrigger("Throw");
                        StartCoroutine(LevelTwoBatThrowRoutine());
                        Shoot();
                    }
                    else
                    {
                        StartCoroutine(LaserEnableDisableRoutine());

                        if (lf.Type == eType.Hero1)
                        {
                            other.GetComponent<Collider>().enabled = false;
                            other.transform.GetChild(0).gameObject.SetActive(false); other.transform.GetChild(01).gameObject.SetActive(true);
                        }
                    }

                }
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.KryptoCrstalguy)
                {
                    if (lf.Type == eType.Hero2)
                    {
                       
                        Anim.SetTrigger("Throw"); StartCoroutine(LevelTwoBatThrowRoutine());
                        Shoot();
                    }
                    else
                    {
                        StartCoroutine(LaserEnableDisableRoutine());
                        if (PlayerPrefs.GetInt("Hero1") == 2)
                        {
                            
                            other.transform.GetChild(0).gameObject.SetActive(false); other.transform.GetChild(01).gameObject.SetActive(true);
                                                  
                            other.GetComponent<Collider>().enabled = false;
                  
                        }
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
                        Shoot();
                    }
                    else
                    {
                        SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().Webshoot);
                        Anim.SetTrigger("Throw"); StartCoroutine(LevelTwoBatThrowRoutine());
                        Shoot();
                    }
                }
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.LaserWall)
                {
                    if (lf.Type == eType.Hero2)
                    {
                        SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().Webshoot);
                        Anim.SetTrigger("Throw"); StartCoroutine(LevelTwoBatThrowRoutine());
                        Shoot();
                    }
                    else
                    {
                        SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().IronShoot);
                        Anim.SetTrigger("Shoot");
                        Shoot();
                    }
                }
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.WarMachine)
                {
                    if (lf.Type == eType.Hero2)
                    {
                        SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().Webshoot);
                        Anim.SetTrigger("Throw"); StartCoroutine(LevelTwoBatThrowRoutine());
                        Shoot();
                    }
                    else
                    {
                        SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().IronShoot);
                        Anim.SetTrigger("Shoot");
                        Shoot();
                    }
                }
                if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Ice)
                {
                    if (lf.Type == eType.Hero2)
                    {
                      

                        Anim.SetTrigger("Throw"); StartCoroutine(LevelTwoBatThrowRoutine());
                        Shoot();
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
                        Shoot();
                    }
                    else
                    {

                        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                       
                       
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
                if (GetComponentInParent<Animator>() != null)
                GetComponentInParent<Animator>().Play("ShootRun");

                StartCoroutine(Shoot6Times());
            }
        }
    }
    public IEnumerator EnemyDeath(GameObject g)
    {
        yield return new WaitForSeconds(.3f);
        g.GetComponentInChildren<Animator>().Play("Death");
       g.GetComponent<Collider>().enabled = false;
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
        if (!GameManager.Instance.IsHulkScene)
        {

            if (!GameManager.Instance.IsIronManScene)
            {
                if (lf.HeroLevel == 2)
                {
                    Instantiate(SpiderNet, NewGunSpawnPoint.position, Quaternion.identity);
                    lf.DummyGun.gameObject.SetActive(false);
                    lf.Power2.gameObject.SetActive(true);
                    yield return new WaitForSeconds(.3f);
                    lf.Power2.gameObject.SetActive(false);
                    lf.DummyGun.gameObject.SetActive(true);
                    SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().IronShoot);
                }
            }
            else
            {
                if (lf.HeroLevel == 2)
                {
                    print("throw");
                    GameObject g = Instantiate(SpiderNet, SpawnPoint.position, Quaternion.identity);
                    // g.transform.DOLocalRotate(new Vector3(0, 90, 0), 0);
                    g.transform.DOScale(new Vector3(.7f, .7f, .7f), 2f);

                }
            }
        }
    
    }

    public void Shoot()
    {
        if (!GameManager.Instance.IsHulkScene)
        {
            if (lf.Type == eType.Hero2)
            {

                if (lf.HeroLevel != 2)
                {
                    GameObject g = Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
                    int i = GetComponentInParent<PerCollsion>().ColelctableCount / 5;

                    g.GetComponent<Projectile>().ProjectileSpeed = g.GetComponent<Projectile>().ProjectileSpeed + (i * .25f);

                }

                if (lf.HeroLevel == 0)
                    SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().BatBlade);

            }

            else
            {
                if (lf.HeroLevel == 0 || lf.HeroLevel == 01)
                {
                    GameObject g1 = Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
                    int i = GetComponentInParent<PerCollsion>().ColelctableCount / 5;

                    g1.GetComponent<Projectile>().ProjectileSpeed = g1.GetComponent<Projectile>().ProjectileSpeed + (i * .25f);
                }
                else
                {
                    
                    GameObject g1 = Instantiate(SpiderNet, SpawnPoint.position, Quaternion.identity);
                    int i = GetComponentInParent<PerCollsion>().ColelctableCount / 5;

                    g1.GetComponent<Projectile>().ProjectileSpeed = g1.GetComponent<Projectile>().ProjectileSpeed + (i * .25f);
                }

            }
        }
        else
        {
            if (lf.HeroLevel == 2)
            {
                GameObject g = Anim.transform.parent.gameObject;
                Invoke("PoofDelay", .4f);
                g.transform.DOLocalJump(new Vector3(0, 0, 35), 5, 1, .4f).OnComplete(() => {
                    g.transform.DOLocalMoveZ(0, 1.8f);
                    Anim.GetComponent<Collider>().enabled = true;
                  
                });
                }

        }

    }
    void PoofDelay()
    {
        poof.Play();
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

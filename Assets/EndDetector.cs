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
    Vector3 StartPos;

    private void Start()
    {
        StartPos = Anim.transform.position;
        lf = GetComponentInParent<LookTowards>();
    }
    public IEnumerator GetOnCarRoutine(GameObject obj)
    {
      
        obj.GetComponent<Collider>().enabled = false;
        GameManager.Instance.p.transform.GetChild(0).transform.DOLocalRotate(new Vector3(0, 0, 0), .2f);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.BatMobile.transform.GetComponent<Animator>().Play("Open");
       
        GameManager.Instance.p.transform.GetComponentInChildren<CircularMovement>().enabled = false;
        GameManager.Instance.BatMobile.transform.parent = GameManager.Instance.p.transform.GetChild(0);
        Anim.transform.DOJump(GameManager.Instance.BatMobile.transform.position, .5f, 1, .3f).OnComplete(() => {
        
            GameManager.Instance.BatMobile.transform.GetComponent<Animator>().Play("Close");
            Anim.gameObject.SetActive(false); GameManager.Instance.BatMobile.transform.DOLocalRotate(new Vector3(0, -90f, 0), .3f).OnComplete(() => {

            FindObjectOfType<Controller>().enabled = true; GameManager.Instance.BatMobile.transform.GetComponent<Animator>().Play("Driving");
                GameManager.Instance.p.MaxSpeed = 9;
            }); }); GameManager.Instance.ZoomEffect();
        Camera.main.transform.DOLocalMoveZ(15f,1f);
        Camera.main.transform.GetChild(0).gameObject.SetActive(true);

        Anim.SetTrigger("Jump");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blue"))
        {
          //  Camera.main.transform.DOLocalRotate(GameManager.Instance.FianlCamPos.transform.localEulerAngles, 1.5f);
         //   Camera.main.transform.DOLocalMove(GameManager.Instance.FianlCamPos.transform.localPosition, 1.5f);
        }
        if(other.gameObject.CompareTag("Car"))
        {
           
            StartCoroutine(GetOnCarRoutine(other.gameObject));
        }


        if (other.gameObject.CompareTag("Enemy"))
        {
            if(other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Wall || other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.KyptoBlock)
            {
                if(lf.Type == eType.Hero2)
                {
                    SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().BatBlade);
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
                    SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().BatBlade);
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
                    Anim.SetTrigger("Throw");
                    Instantiate(Projectile, SpawnPoint.position, Quaternion.identity);
                }
            }
            if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.LaserWall)
            {
                if (lf.Type == eType.Hero2)
                {
                    SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().Webshoot);
                    Anim.SetTrigger("Throw"); 
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
                    Anim.SetTrigger("Throw"); 
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
        //SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().SuperLaser);
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        Laser.gameObject.SetActive(true);
        yield return new WaitForSeconds(LaserTime);
        Laser.gameObject.SetActive(false);
            
    }
}

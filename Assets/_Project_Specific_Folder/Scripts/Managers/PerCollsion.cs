using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Animations;
using MoreMountains.NiceVibrations;
public enum eType
{
    Hero1, 
    Hero2
}
public class PerCollsion : MonoBehaviour
{
    public eType Type;
    [Header("Hero Upgrade Stuffs")]
    public int HeroLevel;
    public RuntimeAnimatorController Current;
    public RuntimeAnimatorController Level2Aniamtor, Level3Animator;
    public GameObject Power1, Power2;
    public GameObject DummyGun;
    [Header("VFX")]
    public ParticleSystem CollectableVFX;
    public ParticleSystem PowerVFX;

    [Header("Tweak")]
    public float SpeedDecreaseAmmount = .25f;
    public int MorphAmmount = 5;
    public int MorphCap = 50;

    public float ScaleAmmount = .3f;



    public ParticleSystem TorneddoFX;
    public Ease ease;

    public float ScaleAmmounts = .15f;
    public int ColelctableCount;
    public Animator anim;
    public GameObject Cape;
    public GameObject CapeFinalPos;
    public float CurrentSpeed,CurrentMaxSpeed;
    public int SizeDownAmmount = 20;

    EndDetector m_DetectorScript;
    float i, j;
    void Start()
    {

      
        if(m_DetectorScript != null)
        {
          m_DetectorScript =  GetComponentInChildren<EndDetector>();
        }


        if (Type == eType.Hero1)
        {
            Hero1Upgrade();
        }
        if (Type == eType.Hero2)
        {
            Hero2Upgrade();
        }

        CurrentSpeed = GameManager.Instance.p.MaxSpeed;
        CurrentMaxSpeed = GameManager.Instance.p.MaxSpeed;
    }

    public void Hero1Upgrade()
    {

        HeroLevel = PlayerPrefs.GetInt("Hero1");
        if (HeroLevel == 1)
        {
            if(Power1 != null)
            Power1.SetActive(true);
            anim.transform.GetComponent<MySDK.Mover>().m_InitialPosition.z=1.86f;
            //Power2.SetActive(true);
            anim.transform.GetComponent<MySDK.Mover>().enabled = true;

            anim.runtimeAnimatorController = Level2Aniamtor;
        }
        if (HeroLevel == 2)
        {
            anim.transform.GetComponent<MySDK.Mover>().enabled = true;
            if (Power1 != null)
            Power1.SetActive(false);
            if (Power2 != null)
             Power2.SetActive(true);
            anim.runtimeAnimatorController = Level2Aniamtor;
        }
    }
    public void Hero2Upgrade()
    {
        
        HeroLevel = PlayerPrefs.GetInt("Hero2");
        if (HeroLevel == 1)
        {
            if (Power1 != null)
                Power1.SetActive(true);
            anim.transform.DOLocalMoveY(0, .2f);
            anim.transform.GetComponent<MySDK.Mover>().m_InitialPosition.y = 0;
            anim.transform.GetComponent<MySDK.Mover>().enabled = true;
            anim.runtimeAnimatorController = Level2Aniamtor;
        }
        if (HeroLevel == 2)
        {
            if(DummyGun != null)
            DummyGun.gameObject.SetActive(true);
            if (Power1 != null)
            Power1.SetActive(true);
            anim.transform.DOLocalMoveY(0, .2f);
            anim.transform.GetComponent<MySDK.Mover>().m_InitialPosition.y = 0;
            anim.transform.GetComponent<MySDK.Mover>().enabled = true;
            anim.runtimeAnimatorController = Level3Animator;
        }
    }
    public IEnumerator OnlyRedScreenRoutine()
    {
       
        UiManager.Instance.FadeIn.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
    
        UiManager.Instance.FadeIn.gameObject.SetActive(false);

    }
    public IEnumerator RedScreenRoutine()
    {
  
        anim.SetBool("Injured", true);
        GameManager.Instance.p.speed -= SpeedDecreaseAmmount; GameManager.Instance.p.MaxSpeed -= SpeedDecreaseAmmount;
        UiManager.Instance.FadeIn.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("Injured", false); 
        UiManager.Instance.FadeIn.gameObject.SetActive(false);
        yield return new WaitForSeconds(.3f);
        GameManager.Instance.p.speed = CurrentSpeed;
        GameManager.Instance.p.MaxSpeed = CurrentMaxSpeed;
    }
    public void DoorSizeDownRoutine()
    {
        StartCoroutine(OnlyRedScreenRoutine());

        float a = transform.GetComponentInChildren<SkinnedMeshRenderer>().GetBlendShapeWeight(0);
        if(a > 0)
        transform.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(0,SizeDownAmmount);

        anim.transform.DOScale(new Vector3(anim.transform.localScale.x - ScaleAmmounts,
              anim.transform.localScale.y - ScaleAmmounts
               , anim.transform.localScale.z - ScaleAmmounts
               ), .3f).SetEase(ease).OnComplete(() => {
                   anim.transform.DOScale(new Vector3(anim.transform.localScale.x + ScaleAmmounts,
                   anim.transform.localScale.y + ScaleAmmounts
                   , anim.transform.localScale.z + ScaleAmmounts
                   ), .3f);
               });

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.Ice)
            {
                if (Type == eType.Hero1)
                {
                    other.gameObject.GetComponent<Collider>().enabled = false;
                    other.transform.parent.GetChild(0).gameObject.SetActive(false);
                    SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                    anim.SetTrigger("Punch");
                    other.gameObject.GetComponent<Enemy>().BrokenPieces.SetActive(true);
                }


            }
        }

        if (other.gameObject.CompareTag("Cash"))
        {
            UiManager.Instance.IncreasePoints(10); other.transform.GetChild(0).gameObject.SetActive(true);
             other.transform.GetComponent<Collider>().enabled = false; other.transform.GetComponent<MeshRenderer>().enabled = false;
        }


        if (Type == eType.Hero1)
        {
            if (other.gameObject.CompareTag("Black"))
            {
             //   GameManager.Instance.Reset();
             //   GetComponentInParent<PathCreation.Examples.PathFollower>().enabled = false;
              //  anim.SetTrigger("Death");
              MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                DoorSizeDownRoutine();

            }
            if (other.gameObject.CompareTag("Blue"))
            {
                other.transform.GetChild(0).gameObject.SetActive(false);
                other.transform.GetChild(01).gameObject.GetComponent<MeshRenderer>().material.color = Color.green; MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            }


        }
        if (Type == eType.Hero2)
        {
            if (other.gameObject.CompareTag("Blue"))
            {
                MMVibrationManager.Haptic(HapticTypes.MediumImpact); DoorSizeDownRoutine();

            }
            if (other.gameObject.CompareTag("Black"))
            {
                other.transform.GetChild(0).gameObject.SetActive(false);
                other.transform.GetChild(01).gameObject.SetActive(false);
                other.transform.GetChild(02).gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            }


        }
        if (Type == eType.Hero1)
        {
            if (other.gameObject.CompareTag("Hero1c"))
            {
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                i += MorphAmmount;
                Scaleup();
                if(i<=MorphCap)
                transform.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(0,i);
                ColelctableCount++;
                if (ColelctableCount % 5 == 0 && ColelctableCount>1)
                {
                    IncreaseProjectileSpeed();
                    // SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().SizeUp);
                    GameManager.Instance.p.MaxSpeed += .25f;
                    MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                    StartCoroutine(EvolveEffectRoutine());
                    anim.transform.DOScale(new Vector3(anim.transform.localScale.x + ScaleAmmount,
                      anim.transform.localScale.y + ScaleAmmount
                       , anim.transform.localScale.z + ScaleAmmount
                       ), .3f).SetEase(ease);
                    CurrentMaxSpeed = GameManager.Instance.p.MaxSpeed;
                }
                //CollectableVFX.Play();

                Destroy(other.gameObject);
            }
          else  if (other.gameObject.CompareTag("Hero2c"))
            {
                print("Superman");
                StartCoroutine(RedScreenRoutine());
            }


        }
        if (Type == eType.Hero2)
        {
            if (other.gameObject.CompareTag("Hero2c"))
            {
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                j += MorphAmmount;
                Scaleup();
                if (j <= MorphCap)
                 transform.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(0, j);
                //CollectableVFX.Play();
                ColelctableCount++;
               
                    if (ColelctableCount % 5 == 0 && ColelctableCount > 1)
                {
                    IncreaseProjectileSpeed();
                    SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().SizeUp);
                    GameManager.Instance.p.MaxSpeed += .25f;
                    MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                    StartCoroutine(EvolveEffectRoutine());
                    anim.transform.DOScale(new Vector3(anim.transform.localScale.x + ScaleAmmount,
                       anim.transform.localScale.y + ScaleAmmount
                        , anim.transform.localScale.z + ScaleAmmount
                        ), .3f).SetEase(ease);

                    CurrentMaxSpeed = GameManager.Instance.p.MaxSpeed;
                }
                Destroy(other.gameObject);
            }

           else if (other.gameObject.CompareTag("Hero1c"))
            {
                StartCoroutine(RedScreenRoutine());
            }
        }
    }

    private void IncreaseProjectileSpeed()
    {

    }

    public IEnumerator EvolveEffectRoutine()
    {
    

        CollectableVFX.gameObject.SetActive(true); yield return new WaitForSeconds(1); CollectableVFX.gameObject.SetActive(false);
    }

    void Scaleup()
    {
        anim.transform.DOScale(new Vector3(anim.transform.localScale.x + ScaleAmmounts,
                      anim.transform.localScale.y + ScaleAmmounts
                       , anim.transform.localScale.z + ScaleAmmounts
                       ), .3f).SetEase(ease).OnComplete(() => {
                           anim.transform.DOScale(new Vector3(anim.transform.localScale.x - ScaleAmmounts,
anim.transform.localScale.y - ScaleAmmounts
, anim.transform.localScale.z - ScaleAmmounts
), .3f);
                       });

   }
}

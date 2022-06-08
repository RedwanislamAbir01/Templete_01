using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using MoreMountains.NiceVibrations;
public enum eType
{
    Hero1, 
    Hero2
}
public class LookTowards : MonoBehaviour
{
    public int HeroLevel;
    public Ease ease;
    public float ScaleAmmount = .3f; public float ScaleAmmounts = .15f;
    public ParticleSystem CollectableVFX , PowerVFX;
    public GameObject Target;
    public eType Type;
    public int ColelctableCount;
    public Animator anim;
    float i, j;
    public GameObject Cape;
    public GameObject CapeFinalPos;

    public float CurrentSpeed,CurrentMaxSpeed;
    public int SizeDownAmmount = 20;
    void Start()
    {
        CurrentSpeed = GameManager.Instance.p.MaxSpeed;
        CurrentMaxSpeed = GameManager.Instance.p.MaxSpeed;
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
        GameManager.Instance.p.speed = .75f; GameManager.Instance.p.MaxSpeed= .75f;
        UiManager.Instance.FadeIn.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("Injured", false); 
        UiManager.Instance.FadeIn.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
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
       
        if(other.gameObject.CompareTag("Cash"))
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
                i += 10;
                Scaleup();
                if(i<=50)
                transform.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(0,i);
                ColelctableCount++;
                if (ColelctableCount % 5 == 0 && ColelctableCount>1)
                {
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
                j += 10;
                Scaleup();
                if (j <= 50)
                    transform.GetComponentInChildren<SkinnedMeshRenderer>().SetBlendShapeWeight(0, j);
                //CollectableVFX.Play();
                ColelctableCount++;
               
                    if (ColelctableCount % 5 == 0 && ColelctableCount > 1)
                {
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

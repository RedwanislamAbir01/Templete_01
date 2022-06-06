using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoreMountains.NiceVibrations;
public class Collsion : MonoBehaviour
{
    public float SpeedIncreasAmmount;
    public GameObject Target;
    public GameObject Hero1, Hero2;
    public Vector3 H1start, H2Start;
    public GameObject Shadow1, Shadow2, Connector;

    public GameObject Hero1Model, Hero2Model;
    public bool StartTapRoutine;
    Vector3 Hero1ModelPos, Hero2ModelPos;

    public GameObject Sphere, Sphere1;
    public GameObject Bazooka;
    public GameObject BatCape;
    public GameObject Boss1, Boss2;
    float DistanceZ;
    public Ease ease;
    public GameObject TargetCapePos;
    Vector3 StartCapePos;
    Vector3 StartCapeRot;
    public Transform ShootPos, ShootPos1;
    public GameObject FinalShootProjectile , FinalShootProjectile1;
    public bool Flying;
    Vector3 camStartPos;
    Quaternion camStartRot;
    float CurrentSpeed, CurrentMaxSpeed;
    private void Awake()
    {
 
        camStartPos = Camera.main.transform.localPosition;
        camStartRot = Camera.main.transform.localRotation;

        Hero1ModelPos = Hero1Model.transform.position; Hero2ModelPos = Hero2Model.transform.localPosition;
        H1start = Hero1.transform.localPosition;
        H2Start = Hero2.transform.localPosition;
        StartCapePos = BatCape.transform.localPosition;
        StartCapeRot = BatCape.transform.localEulerAngles;

    }
    private void Update()
    {
        if (StartTapRoutine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (UiManager.Instance.timerInitvalue < 1f)
                {
                    UiManager.Instance.timerInitvalue += 0.21f;
                    Sphere.transform.DOScale(new Vector3(Sphere.transform.localScale.x + (UiManager.Instance.timerInitvalue /15), Sphere.transform.localScale.y
                         + (UiManager.Instance.timerInitvalue /15), Sphere.transform.localScale.z
                          + (UiManager.Instance.timerInitvalue /15)), .1f);
                    UiManager.Instance.Timer.fillAmount = UiManager.Instance.timerInitvalue;
                    Sphere1.transform.DOScale(new Vector3(Sphere1.transform.localScale.x + (UiManager.Instance.timerInitvalue /15f), Sphere1.transform.localScale.y
                         + (UiManager.Instance.timerInitvalue /15), Sphere1.transform.localScale.z
                          + (UiManager.Instance.timerInitvalue /15)), .1f);
                    UiManager.Instance.Timer.fillAmount = UiManager.Instance.timerInitvalue;
                    MMVibrationManager.Haptic(HapticTypes.LightImpact);
                    
                    Camera.main.transform.DOShakePosition(1.5f,.2f);
                    Camera.main.DOFieldOfView(60, 2);
                }
            }

            if (UiManager.Instance.timerInitvalue > 0f)
            {
                UiManager.Instance.timerInitvalue -= 0.0075f;
                UiManager.Instance.Timer.fillAmount = UiManager.Instance.timerInitvalue;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {

            StartCoroutine(GetOnCarRoutine(other.gameObject));
        }
        if (other.gameObject.CompareTag("Exit"))
        {
            other.gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(OnExitRoutine());
        }
        if (other.gameObject.CompareTag("FinishLine"))
        {
          
            if (transform.localEulerAngles.y > -90 && transform.localEulerAngles.y <= 90)
            {
                Hero1.transform.DOLocalMove(H1start, .3f);
                Hero2.transform.DOLocalMove(H2Start, .3f);
  

            }
            else
            {
                Hero2.transform.DOLocalMove(H1start, .3f);
                Hero1.transform.DOLocalMove(H2Start, .3f);
                Hero2.transform.DOLocalRotate (new Vector3(0,0,0), .3f);
                Hero1.transform.DOLocalRotate(new Vector3(0, 0, 0), .3f);
            }
                Connector.SetActive(false);
            GameManager.Instance.GameEnd = true;
            Boss1.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Taunt");
            StartCoroutine(StopRountine());
            transform.DOLocalRotate(new Vector3(0, 0, 0), .2f);
            Target.transform.DOLocalRotate(new Vector3(0, 0, 0), .2f);
            transform.DOLocalMove(new Vector3(0, 0.88f, 0), .2f);
            //transform.GetComponent<Controller>().enabled = false;

            StartCoroutine(TapFastOff());
        }

        if (other.gameObject.CompareTag("Finish"))
        {

            GameManager.Instance.Fly.Play();
            Fly();
            GameManager.Instance.ZoomEffect();



        }
        if (other.gameObject.CompareTag("Land"))
        {
            GameManager.Instance.Fly.Stop();
            GameManager.Instance.p.MaxSpeed -= SpeedIncreasAmmount; 
            GameManager.Instance.p.speed = CurrentSpeed;
            GameManager.Instance.p.MaxSpeed = CurrentMaxSpeed;
            Camera.main.transform.DOLocalMove(camStartPos, .2f);
            Camera.main.transform.DOLocalRotate(camStartRot.eulerAngles, .2f);
            Flying = false;
          
            BatCape.transform.DOLocalRotate(StartCapeRot, .2f);
            BatCape.transform.DOLocalMove(StartCapePos, .01f);
            Hero2Model.transform.DOLocalMove(Hero2ModelPos, .1f);
            Hero2Model.GetComponent<Animator>().SetBool("Hang", false); Hero1Model.GetComponent<Animator>().SetBool("Hang", false);
            Target.transform.DOLocalMoveX(0, .1f);
            transform.DOLocalMoveX(0, .1f);
            GetComponent<Controller>().enabled = false;
            Target.transform.GetChild(0).DOLocalMove(H1start, .3f); Target.transform.GetChild(01).transform.DOLocalMove(H2Start, .3f);
            Hero1.transform.DOLocalMove(H1start, .3f);
            Hero2.transform.DOLocalMove(H2Start, .3f).OnComplete(() =>
            {
                Camera.main.transform.GetChild(0).gameObject.SetActive(false);
                Shadow1.gameObject.SetActive(true); Shadow2.gameObject.SetActive(true); Connector.gameObject.SetActive(true);
              
         
                Hero1.GetComponent<LookTowards>().enabled = true; Hero2.GetComponent<LookTowards>().enabled = true;

            });
        }
    }

    void Fly()
    {
        CurrentMaxSpeed = GameManager.Instance.p.MaxSpeed;
        CurrentMaxSpeed = GameManager.Instance.p.speed;
        Camera.main.transform.DOLocalMove(new Vector3(0,14.2f , -20.4f), .2f);
        Camera.main.transform.DOLocalRotate(new Vector3(17.66f , -.031f , -.089f), .2f);
        GameManager.Instance.p.MaxSpeed += SpeedIncreasAmmount;
        Flying = true;
       
        Hero1.GetComponent<LookTowards>().enabled = false;
        Hero2.GetComponent<LookTowards>().enabled = false;

        BatCape.transform.DOLocalRotate(TargetCapePos.transform.localEulerAngles, .2f);
        BatCape.transform.DOLocalMove(TargetCapePos.transform.localPosition, .01f);
        Camera.main.transform.GetChild(0).gameObject.SetActive(true);
        transform.DOLocalRotate(new Vector3(0, 0, 0), 0);
        Target.transform.DOLocalRotate(new Vector3(0, 0, 0), 0);
        Hero1.transform.DOLocalRotate(new Vector3(0, -0, 0), 0f); Hero2.transform.DOLocalRotate(new Vector3(0, -0, 0), 0f);

        Hero1.transform.DOLocalMoveY(5.98f, .3f);
        Hero1.transform.DOLocalMoveX(0, .3f);
        Hero2Model.GetComponent<Animator>().SetBool("Hang", true);
        Hero1Model.GetComponent<Animator>().SetBool("Hang", true);
        //Hero2Model.transform.DOLocalJump(new Vector3(0.26f, -5.75f, 2.53f), 4,1, .2f);
        Hero2.transform.DOLocalMove(new Vector3(0.55f, -1.31f, 2.08f), 0.2f);

        Connector.gameObject.SetActive(false);
        Hero2.transform.DOLocalMoveX(0.33f, .5f).OnComplete(() =>
        {

            Hero1.transform.DOLocalRotate(new Vector3(0, -0, 0), .3f); Hero2.transform.DOLocalRotate(new Vector3(0, -0, 0), .1f);
            Target.transform.GetChild(0).DOLocalRotate(new Vector3(0, -0, 0), .1f); Target.transform.GetChild(01).transform.DOLocalRotate(new Vector3(0, -0, 0), .1f);
            GetComponent<Controller>().enabled = true;
        });

    }

    public IEnumerator StopRountine()
    {
        Camera.main.transform.DOLocalMove(GameManager.Instance.FianlCamPos.transform.localPosition, 1);
        Camera.main.transform.DOLocalRotate(GameManager.Instance.FianlCamPos.transform.localEulerAngles, 1);
        yield return new WaitForSeconds(.3f);
        GameManager.Instance.p.MaxSpeed = .3f;
        GameManager.Instance.p.speed = .3f;

        yield return new WaitForSeconds(.2f);
        GameManager.Instance.p.enabled = false;



        Hero1Model.transform.parent.DOLocalRotate(new Vector3(Hero1Model.transform.parent.localEulerAngles.x, -16, Hero1Model.transform.parent.localEulerAngles.z), .15f);
        Hero2Model.transform.DOLocalRotate(new Vector3(Hero2Model.transform.localEulerAngles.x, 16, Hero2Model.transform.localEulerAngles.z), .15f);


        Hero1.transform.GetComponent<LookTowards>().anim.Play("Aim");
        Hero2.transform.GetComponent<LookTowards>().anim.Play("Aim");
        Bazooka.gameObject.SetActive(true);
        Sphere1.gameObject.SetActive(true); 
        StartTapRoutine = true;
        UiManager.Instance.TapFastPanel.SetActive(true);
    }

    public IEnumerator TapFastOff()
    {
        yield return new WaitForSeconds(1);
        Hero1.GetComponent<LookTowards>().PowerVFX.gameObject.SetActive(true); Hero2.GetComponent<LookTowards>().PowerVFX.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        StartTapRoutine = false;
        Hero1.transform.GetComponent<LookTowards>().anim.SetTrigger("Shoot");
        // Hero2.transform.GetComponent<LookTowards>().anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(.5f);
        Hero2.transform.GetComponent<LookTowards>().anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(.5f); 
        Sphere.SetActive(false);
        Sphere1.SetActive(false);
        FinalShoot();
    
       
        UiManager.Instance.TapFastPanel.SetActive(false);
        Camera.main.transform.DOLocalMoveZ(-5.8f, .3f);


    }
    void FinalShoot()
    {
        MMVibrationManager.Haptic(HapticTypes.LightImpact);
        GameObject Projectile1 = Instantiate(FinalShootProjectile, ShootPos.position, Quaternion.identity);
        Projectile1.transform.DOScale(Sphere.transform.localScale / 2.5f, 0);
        GameObject Projectile2 = Instantiate(FinalShootProjectile1, ShootPos1.position, Quaternion.identity);
        Projectile2.transform.DOScale(Sphere.transform.localScale / 2.5f, 0);
    }

    public IEnumerator ShootBossRoutine()
    {
        Camera.main.transform.DOLocalMove(new Vector3(1.83f, 1.84f, -7.12f), .3f);
        Camera.main.transform.DOLocalRotate(new Vector3(14.21f, -18.791f, 1.535f), .3f);
        Camera.main.transform.parent = Boss1.transform;
     


        yield return new WaitForSeconds(0f);

        if (UiManager.Instance.timerInitvalue >= 0f && UiManager.Instance.timerInitvalue < 0.2f)
        {
            print("ok");
   
            DistanceZ = -1.21f;
            Boss1.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hit"); Boss2.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hit");
            Boss1.transform.DOLocalMoveZ(DistanceZ, .8f).SetEase(ease).OnComplete(() => { Boss1.transform.GetChild(0).GetComponent<Animator>().Play("Death"); });
            Camera.main.transform.parent = Boss1.transform;
      
            Boss2.transform.DOLocalMoveZ(DistanceZ, .6f).SetEase(ease).OnComplete(() => { Boss2.transform.GetChild(0).GetComponent<Animator>().Play("Death"); });
        }
        else if (UiManager.Instance.timerInitvalue >= 0.2f && UiManager.Instance.timerInitvalue < 0.4f)
        {
            Camera.main.transform.parent = Boss1.transform;
            Boss1.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hit");
            Boss2.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hit");
            print("good");
            DistanceZ = 5.86f;
            Boss1.transform.DOLocalMoveZ(DistanceZ, 1.3f).SetEase(ease).OnComplete(() => { Boss1.transform.GetChild(0).GetComponent<Animator>().Play("Death"); });
            Boss2.transform.DOLocalMoveZ(DistanceZ, .8f).SetEase(ease).OnComplete(() => { Boss2.transform.GetChild(0).GetComponent<Animator>().Play("Death"); });
        }
        else if (UiManager.Instance.timerInitvalue >= 0.4f && UiManager.Instance.timerInitvalue < 0.6f)
        {
            Camera.main.transform.parent = Boss1.transform;
            Boss1.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hit"); Boss2.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hit");
            print("nice"); DistanceZ = 13.33f; Boss1.transform.DOLocalMoveZ(DistanceZ, 1.5f).SetEase(ease).OnComplete(() => { Boss1.transform.GetChild(0).GetComponent<Animator>().Play("Death"); });
            Boss2.transform.DOLocalMoveZ(DistanceZ, 1.5f).SetEase(ease).OnComplete(() => { Boss2.transform.GetChild(0).GetComponent<Animator>().Play("Death"); });
        }
        else if (UiManager.Instance.timerInitvalue >= 0.6f && UiManager.Instance.timerInitvalue < 0.8f)
        {
            Camera.main.transform.parent = Boss1.transform;
            Boss1.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hit"); Boss2.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hit");
            print("awesome"); DistanceZ = 20.88f; Boss1.transform.DOLocalMoveZ(DistanceZ, 2f).SetEase(ease).OnComplete(() => { Boss1.transform.GetChild(0).GetComponent<Animator>().Play("Death"); });
            Boss2.transform.DOLocalMoveZ(DistanceZ, 2f).SetEase(ease).OnComplete(() => { Boss2.transform.GetChild(0).GetComponent<Animator>().Play("Death"); });
        }
        else if (UiManager.Instance.timerInitvalue >= 0.8f)
        {
            Camera.main.transform.parent = Boss1.transform;
            Boss1.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hit"); Boss2.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hit");
            print("perfect"); DistanceZ = 28.72f;
            Boss1.transform.DOLocalMoveZ(DistanceZ,2.8f).SetEase(ease).OnComplete(() => { Boss1.transform.GetChild(0).GetComponent<Animator>().Play("Death"); });
            Boss2.transform.DOLocalMoveZ(DistanceZ,2.8f).SetEase(ease).OnComplete(() => { Boss2.transform.GetChild(0).GetComponent<Animator>().Play("Death"); });
        }
        else if (  UiManager.Instance.timerInitvalue <=0.19f)
        {
     
            print("ok");
            DistanceZ = -4.4f;
            Boss1.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hit"); Boss2.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hit");
            Boss1.transform.DOLocalMoveZ(DistanceZ, .8f).SetEase(ease).OnComplete(() => { Boss1.transform.GetChild(0).GetComponent<Animator>().Play("Death"); });
            Camera.main.transform.parent = Boss1.transform;

            Boss2.transform.DOLocalMoveZ(DistanceZ, .6f).SetEase(ease).OnComplete(() => { Boss2.transform.GetChild(0).GetComponent<Animator>().Play("Death"); });
        }
        yield return new WaitForSeconds(5f);
        UiManager.Instance.CompleteUI.SetActive(true);
    }


    public IEnumerator GetOnCarRoutine(GameObject obj)
    {
     
     
        transform.DOLocalRotate(new Vector3(0, 0, 0), .2f);
        Target.transform.DOLocalRotate(new Vector3(0, 0, 0), .2f);
        transform.DOLocalMove(new Vector3(0, 0.88f, 0), .2f);

        Connector.SetActive(false);
        Hero1.transform.DOLocalMove(H1start, .1f);
        Hero2.transform.DOLocalMove(H2Start, .1f);
        yield return new WaitForSeconds(.1f);
        GameManager.Instance.p.enabled = false;
        Hero2.transform.DOLocalRotate(new Vector3(0, 0, 0), 0f); Hero1.transform.DOLocalRotate(new Vector3(0, 0, 0), 0f);
      
        GameManager.Instance.BatMobile.transform.GetComponent<Animator>().Play("Open");
        GameManager.Instance.BatMobile.transform.DOLocalMoveX(0, .3f);
       
        GameManager.Instance.BatMobile.transform.parent = GameManager.Instance.p.transform.GetChild(0);
        Hero2.transform.DOJump(GameManager.Instance.BatMobile.transform.position, .5f, 1, .4f);
        yield return new WaitForSeconds(.1f);
        GameManager.Instance.p.enabled = true;
        Hero1.transform.DOJump(GameManager.Instance.BatMobile.transform.position, .5f, 1, .5f).OnComplete(() => {
            transform.GetComponent<Controller>().enabled = true;
            GameManager.Instance.BatMobile.transform.GetComponent<Animator>().Play("Close");
            Hero2.gameObject.SetActive(false);
            Hero1.gameObject.SetActive(false);
            GameManager.Instance.BatMobile.transform.DOLocalRotate(new Vector3(0, -90f, 0), .3f).OnComplete(() => {

               
                GameManager.Instance.BatMobile.transform.GetComponent<Animator>().Play("Driving");
                GameManager.Instance.p.MaxSpeed = 9;
                Camera.main.transform.GetChild(0).gameObject.SetActive(true);
            });
        });
        GameManager.Instance.ZoomEffect();
        Camera.main.transform.DOLocalMoveZ(5f, 1.5f);

        Hero1Model.GetComponent<Animator>().SetTrigger("Jump");
        Hero2Model.GetComponent<Animator>().SetTrigger("Jump");


    }
    public IEnumerator OnExitRoutine()
    {
        transform.GetComponent<Controller>().enabled = false;
        Camera.main.transform.GetChild(0).gameObject.SetActive(false);
        //  GameManager.Instance.p.MaxSpeed -= SpeedIncreasAmmount;
        GameManager.Instance.p.speed = 0;
        GameManager.Instance.p.MaxSpeed = 0; GameManager.Instance.p.MinSpeed = 0; GameManager.Instance.p.IncreazseMultiplier = 0;
        GameManager.Instance.BatMobile.transform.DORotate(new Vector3(0, 54.4f, 0), .5f).OnComplete(() =>
        {
            GameManager.Instance.BatMobile.transform.GetComponent<Animator>().Play("Open");
            GameManager.Instance.Bat.transform.GetComponent<Animator>().Play("Open");
        });

        // GameManager.Instance.BatMobile.transform.DOLocalMoveZ(11.7f, .8f).OnComplete(() =>
        //   {

        // GameManager.Instance.BatMobile.gameObject.SetActive(false);
        // });
        yield return new WaitForSeconds(1.5f);
        Hero1.SetActive(true); Hero2.SetActive(true);
        //  GameManager.Instance.Fly.Stop();


        Camera.main.transform.DOLocalMove(camStartPos, .2f);
        Camera.main.transform.DOLocalRotate(camStartRot.eulerAngles, .2f);


        BatCape.transform.DOLocalRotate(StartCapeRot, .2f);
        BatCape.transform.DOLocalMove(StartCapePos, .01f);
        Hero2Model.transform.DOLocalMove(Hero2ModelPos, .1f);
        Hero1Model.GetComponent<Animator>().SetTrigger("Jump"); Hero2Model.GetComponent<Animator>().SetTrigger("Jump");
        Target.transform.DOLocalMoveX(0, .1f);
        transform.DOLocalMoveX(0, .1f);
        GetComponent<Controller>().enabled = false;

        //Target.transform.GetChild(0).DOLocalMove(new Vector3(H1start.x, H1start.y, H1start.z + 10), .3f);
        Target.transform.GetChild(01).transform.DOLocalMove(H2Start, .3f);

        Hero1.transform.DOLocalJump(new Vector3(H1start.x, H1start.y, H1start.z + 10), .5f, 1, .3f);
        GameManager.Instance.BatMobile.transform.DOLocalMove(new Vector3(0, .21f, 0), .3f);
        transform.DOLocalMoveZ(5.6f, .3f);
        Hero2.transform.DOLocalJump(new Vector3(H2Start.x, H2Start.y, H2Start.z + 10), .5f, 1, .3f).OnComplete(() =>
        {
            Hero1.transform.DOLocalMoveZ(0, .3f); Hero2.transform.DOLocalMoveZ(0, .3f);
            Camera.main.transform.DOLocalMoveZ(-14.25f, .3f);
            GameManager.Instance.BatMobile.transform.parent = null;
            GameManager.Instance.p.speed = 1;
            GameManager.Instance.p.MaxSpeed = 3;
            Hero1Model.GetComponent<Animator>().Play("Run"); Hero2Model.GetComponent<Animator>().Play("Run");

            Shadow1.gameObject.SetActive(true); Shadow2.gameObject.SetActive(true);
            //Connector.transform.DOLocalMove(new Vector3(-0.18f, .21f,10), 0f);
            Connector.gameObject.SetActive(true);
        

            Hero1.GetComponent<LookTowards>().enabled = true; Hero2.GetComponent<LookTowards>().enabled = true;

        }
        );

    }
}

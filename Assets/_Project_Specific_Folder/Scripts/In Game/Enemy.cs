using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using MoreMountains.NiceVibrations;
public enum eEnemyType
{
   Wall ,
   Ice ,
   KyptoBlock,
   KryptoCrstalguy, 
   ShieldGuy, 
   Lava,
   BrickWall,
   LaserWall,
   WarMachine,
   Lizard, 
   Rock, 
   ElectricWall, Loki , 
   Hulk



}
public class Enemy : MonoBehaviour
{
    public eEnemyType EnemyType;
    public GameObject WaterSpill ;
    public GameObject Rope , Net;
    public GameObject BrokenPieces;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {


            if ( EnemyType == eEnemyType.KryptoCrstalguy )
            {
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);

                if (other.GetComponent<PerCollsion>().Type == eType.Hero1)
                {

                    other.GetComponent<PerCollsion>().anim.SetTrigger("Death");
                    other.transform.DOLocalMoveY(-2.07f, .2f);
                    GameManager.Instance.Reset();
                }


            }

            if ( EnemyType == eEnemyType.ShieldGuy)
            {
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                if (other.GetComponent<PerCollsion>().Type == eType.Hero2)
                {

                    other.GetComponent<PerCollsion>().anim.SetTrigger("Death");
                    other.transform.DOLocalMoveY(-2.07f, .2f);
                    GameManager.Instance.Reset();
                }


            }
            if (EnemyType == eEnemyType.ElectricWall)
            {
                if (other.GetComponent<PerCollsion>().Type == eType.Hero2)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    other.GetComponent<PerCollsion>().anim.SetTrigger("Death");
                    other.transform.DOLocalMoveY(-2.07f, .2f);
                    GameManager.Instance.Reset();
                }

          
                if (other.GetComponent<PerCollsion>().Type == eType.Hero1)
                {

                   
                }


            }
            if (EnemyType == eEnemyType.Rock)
            {
                if (other.GetComponent<PerCollsion>().Type == eType.Hero2)
                {
                   
                }


                if (other.GetComponent<PerCollsion>().Type == eType.Hero1)
                {
                  
                        other.GetComponent<PerCollsion>().anim.SetTrigger("Death");
                        other.transform.DOLocalMoveY(-2.07f, .2f);
                        GameManager.Instance.Reset();
                    

                }


            }

            if (EnemyType == eEnemyType.BrickWall  || EnemyType == eEnemyType.Wall || EnemyType == eEnemyType.KyptoBlock  )
            {
                if (other.GetComponent<PerCollsion>().Type == eType.Hero2)
                {
                    other.GetComponent<PerCollsion>().anim.SetTrigger("Death");
                    other.transform.DOLocalMoveY(-2.07f, .2f);
                    GameManager.Instance.Reset();
                }
               
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                if (other.GetComponent<PerCollsion>().Type == eType.Hero1)
                {

                    other.GetComponent<PerCollsion>().anim.SetTrigger("Death");
                    other.transform.DOLocalMoveY(-2.07f, .2f);
                    GameManager.Instance.Reset();
                }


            }
            if (EnemyType == eEnemyType.Ice)
            {
                if (other.GetComponent<PerCollsion>().Type == eType.Hero2)
                {
                    other.GetComponent<PerCollsion>().anim.SetTrigger("Death");
                    other.transform.DOLocalMoveY(-2.07f, .2f);
                    GameManager.Instance.Reset();
                }

                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
    


            }
            if ( EnemyType == eEnemyType.Wall )
            {
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                if (other.GetComponent<PerCollsion>().Type == eType.Hero2)
                {
                    other.GetComponent<PerCollsion>().anim.SetTrigger("Death");
                    other.transform.DOLocalMoveY(-2.07f, .2f);
                    GameManager.Instance.Reset();
                }
                  

                else
                {
                   
                    other.GetComponent<PerCollsion>().anim.SetTrigger("Death");
                    other.transform.DOLocalMoveY(-2.07f, .2f);
                    GameManager.Instance.Reset();
                }
            }
          

            if (EnemyType == eEnemyType.Lizard)
            {
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                this.GetComponentInChildren<Animator>().Play("Attack");
                other.transform.DOLocalMoveY(-2.07f, .2f);
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                GameManager.Instance.Reset();
                if (other.GetComponent<PerCollsion>().Type == eType.Hero1)
                {
                  
                    other.GetComponent<PerCollsion>().anim.SetTrigger("Death");
                    GameManager.Instance.Reset(); other.transform.DOLocalMoveY(-2.07f, .2f);
                }
            }
        



            if (EnemyType == eEnemyType.ShieldGuy || EnemyType == eEnemyType.KryptoCrstalguy || EnemyType == eEnemyType.WarMachine)
            {
                SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                this.GetComponentInChildren<Animator>().Play("Attack"); 
                other.transform.DOLocalMoveY(-2.07f, .2f);
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                GameManager.Instance.Reset();
                other.gameObject.GetComponentInParent<PerCollsion>().anim.SetTrigger("Death");
            }
            if (EnemyType == eEnemyType.Lava)
            {
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                if (other.GetComponent<PerCollsion>().Type == eType.Hero2)
                {
                    if (other.GetComponent<PerCollsion>().HeroLevel == 0)
                    {
                      
                        other.GetComponent<PerCollsion>().anim.SetTrigger("Death");
                        GameManager.Instance.Reset(); other.transform.DOLocalMoveY(-2.07f, .2f);
                    }
                }
                if (other.GetComponent<PerCollsion>().Type == eType.Hero1)
                {
                    if (other.GetComponent<PerCollsion>().HeroLevel == 0)
                    {
                      
                        other.GetComponent<PerCollsion>().anim.SetTrigger("Death");
                        GameManager.Instance.Reset(); other.transform.DOLocalMoveY(-2.07f, .2f);
                    }
                }
            }
         
        }
    }
}

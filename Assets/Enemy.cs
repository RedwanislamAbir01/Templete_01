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
   Lizard



}
public class Enemy : MonoBehaviour
{
    public eEnemyType EnemyType;
    public GameObject WaterSpill ;
    public GameObject Rope;
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

                if (other.GetComponent<LookTowards>().Type == eType.Hero1)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    other.GetComponentInParent<Collsion>().Hero2Model.GetComponent<Animator>().SetBool ("Injured", true);
                    print("bat");
                }


            }

            if ( EnemyType == eEnemyType.ShieldGuy || EnemyType == eEnemyType.Lava)
            {

                if (other.GetComponent<LookTowards>().Type == eType.Hero2)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    other.GetComponentInParent<Collsion>().Hero1Model.GetComponent<Animator>().SetBool("Injured", true);
                }


            }

            if (EnemyType == eEnemyType.BrickWall  || EnemyType == eEnemyType.Wall || EnemyType == eEnemyType.KyptoBlock ||EnemyType == eEnemyType.Ice )
            {
                print("wall");
                other.GetComponent<LookTowards>().DoorSizeDownRoutine();

            }


                if (EnemyType == eEnemyType.Lizard)
            {
                SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().EnemyHitPlayer);
                this.GetComponentInChildren<Animator>().Play("Attack");
                other.transform.DOLocalMoveY(-2.07f, .2f);
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                GameManager.Instance.Reset();
                if (other.GetComponent<LookTowards>().Type == eType.Hero1)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    other.GetComponent<LookTowards>().anim.SetTrigger("Death");
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
                other.gameObject.GetComponentInParent<LookTowards>().anim.SetTrigger("Death");
            }
            if (EnemyType == eEnemyType.Lava)
            {
                if (other.GetComponent<LookTowards>().Type == eType.Hero2)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    other.GetComponent<LookTowards>().anim.SetTrigger("Death");
                    GameManager.Instance.Reset(); other.transform.DOLocalMoveY(-2.07f, .2f);
                }
            }
         
        }
    }
}

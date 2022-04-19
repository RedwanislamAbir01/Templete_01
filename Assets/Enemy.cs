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
   Lava



}
public class Enemy : MonoBehaviour
{
    public eEnemyType EnemyType;
    public GameObject WaterSpill ;
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


            if (EnemyType == eEnemyType.Wall || EnemyType == eEnemyType.KryptoCrstalguy || EnemyType == eEnemyType.KyptoBlock)
            {

                if (other.GetComponent<LookTowards>().Type == eType.Hero1)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    other.GetComponentInParent<Collsion>().Hero2Model.GetComponent<Animator>().SetBool ("Injured", true);
                    print("bat");
                }


            }

            if (EnemyType == eEnemyType.Ice || EnemyType == eEnemyType.ShieldGuy || EnemyType == eEnemyType.Lava)
            {

                if (other.GetComponent<LookTowards>().Type == eType.Hero2)
                {
                    MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                    other.GetComponentInParent<Collsion>().Hero1Model.GetComponent<Animator>().SetBool("Injured", true);
                }


            }








            if (EnemyType == eEnemyType.ShieldGuy || EnemyType == eEnemyType.KryptoCrstalguy)
            {
               this.GetComponentInChildren<Animator>().Play("Attack"); 
                other.transform.DOLocalMoveY(-2.07f, .2f);
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                GameManager.Instance.Reset();
            }
            if (EnemyType != eEnemyType.Lava)
            {
                other.gameObject.GetComponent<LookTowards>().anim.SetTrigger("Death");
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                other.transform.DOLocalMoveY(-2.07f, .2f);
                
                GameManager.Instance.Reset();
            }
            else
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

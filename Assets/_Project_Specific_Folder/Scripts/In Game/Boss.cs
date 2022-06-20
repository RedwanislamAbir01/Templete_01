using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
using DG.Tweening;
public class Boss : MonoBehaviour
{
    public ParticleSystem ParticleFX;
    public int CurrentBossLevel;
     int BossLevel;
    public Animator Anim;
    public GameObject[] Meshes;
    private void Start()
    {
        Meshes[GameManager.Instance.BossLevel-1].SetActive(true);
        Anim = GetComponentInChildren<Animator>();
      
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
            GameManager.Instance.WallCollidedWith++;
            Camera.main.transform.DOShakePosition(.5f, .15f);
           
            SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().TankHit);
            collision.gameObject.GetComponent<Wall>().EnableRb();
            ParticleFX.Play();
        
        }
    }


}

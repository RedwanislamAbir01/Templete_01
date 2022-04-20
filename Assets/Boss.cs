using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
using DG.Tweening;
public class Boss : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            Camera.main.transform.DOShakePosition(.5f, .15f);
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
            SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().TankHit);
            collision.gameObject.GetComponent<Wall>().EnableRb();
        }
    }
}

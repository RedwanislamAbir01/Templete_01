using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
public class Boss : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
            SoundManager.SharedManager().PlaySFX(SoundManager.SharedManager().TankHit);
            collision.gameObject.GetComponent<Wall>().EnableRb();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class LastProjectile : MonoBehaviour
{
    public float Speed = 10;
    public GameObject Explosion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Boss"))
        {
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            Instantiate(Explosion, other.transform.position, Quaternion.identity);
            FindObjectOfType<Collsion>().StartCoroutine(FindObjectOfType<Collsion>().ShootBossRoutine());
            Destroy(gameObject);
        }
    }
}

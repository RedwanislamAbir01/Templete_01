using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSaal : MonoBehaviour
{
    public float ProjectileSpeed;
    public float DestroyTime;
    public GameObject HitVFX;
    public GameObject CrystalSpread;
    public GameObject DestroyVFX;
    [SerializeField] bool isBatMobile;
    private void OnEnable()
    {
        Destroy(gameObject, DestroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.right * ProjectileSpeed * Time.deltaTime);
    }
}

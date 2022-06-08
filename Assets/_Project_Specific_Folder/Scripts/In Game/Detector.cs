using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace KaijuRun
{
    public class Detector : MonoBehaviour
    {
        public ParticleSystem Hit;
        public Rigidbody[] Rigidbodies;
        public float Force = 10, Radius = 10;
        private void OnTriggerEnter(Collider other)
        {

        }
        private void Start()
        {
            Camera.main.transform.DOShakePosition(.5f, .5f);
            Rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in Rigidbodies)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                rb.AddExplosionForce(Force, Vector3.right, Radius);
            }
        }
   
    }
    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Rigidbody[] Rigidbodies;
    public float Force = 10 , Radius  =10 ;
    public Color color;

    private void Start()
    {
        Rigidbodies = GetComponentsInChildren<Rigidbody>();
        transform.GetChild(transform.childCount - 1).gameObject.GetComponent<MeshRenderer>().material.color = color;
        transform.GetChild(transform.childCount - 1).gameObject.GetComponent<MeshRenderer>().material.color = color;
        foreach (Rigidbody rb in Rigidbodies)
        {
            rb.gameObject.GetComponent<MeshRenderer>().material.color = color;
        }
    }

    public void EnableRb()
    {
        transform.GetChild(transform.childCount - 1).gameObject.AddComponent<BoxCollider>();
        transform.GetChild(transform.childCount - 1).gameObject.AddComponent<Rigidbody>();
        transform.GetChild(0).gameObject.SetActive(false);
        foreach (Rigidbody rb in Rigidbodies)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.AddExplosionForce(Force, Vector3.back,Radius);
        }
    }
}

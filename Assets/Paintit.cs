using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintit : MonoBehaviour
{
    public bool a;
    public PaintIn3D.P3dHitNearby p;

    private void Update()
    {

        if (a)
            p.ManuallyHitNow();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            p.ManuallyHitNow();print("hit");
        }
    }
}

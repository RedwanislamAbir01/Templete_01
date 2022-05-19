using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
            Destroy(other.gameObject);
    }
}

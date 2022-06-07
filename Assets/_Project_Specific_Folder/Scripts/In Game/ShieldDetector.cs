using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDetector : MonoBehaviour
{
    public bool Enable;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<Enemy>().EnemyType == eEnemyType.ShieldGuy)
            {
                Enable = true;
             
            }
        }








    }
    }

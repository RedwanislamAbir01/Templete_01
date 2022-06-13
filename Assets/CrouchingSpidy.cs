using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchingSpidy : MonoBehaviour
{
    public GameObject Wings;

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("Hero2") != 0)
        {
            Wings.SetActive(true);
        }
        else
        {
            Wings.SetActive(false);
        }


    }
}

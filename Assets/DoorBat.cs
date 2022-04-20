using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum eTypes
{
    BatIcon , 
    Joker  
}
public class DoorBat : MonoBehaviour
{
    public eTypes type;
    public GameObject[] A;
    void Start()
    {
        if(type == eTypes.BatIcon)
        {
            A[0].gameObject.SetActive(true); A[01].gameObject.SetActive(false);
        }
        if (type == eTypes.Joker)
        {
            A[1].gameObject.SetActive(true); A[0].gameObject.SetActive(false);
        }
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

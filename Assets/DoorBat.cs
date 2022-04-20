using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum eTypes
{
    BatIcon , 
    Joker, 
    Car
}
public class DoorBat : MonoBehaviour
{
    public eTypes type;
    public SpriteRenderer s;
    public Sprite[] a;
    void Start()
    {
        if(type == eTypes.BatIcon)
        {
            s.sprite = a[0];
        }
        if (type == eTypes.Joker)
        {
            s.sprite = a[01];
        }
        if (type == eTypes.Car)
        {
            s.sprite = a[02];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForwar : MonoBehaviour
{
    private Vector3 moveDirection = Vector3.forward;
    PlayerController p;
    public float Speed = 20;
    void Start()
    {
        p = GetComponentInChildren<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * Speed * Time.deltaTime);     
    }
}

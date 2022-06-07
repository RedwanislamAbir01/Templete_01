using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ClothTransforms : MonoBehaviour
{
    public Vector3 ScaleValue;
    public Vector3 Pos;
    void Start()
    {
        transform.DOScale (ScaleValue, .1f);
        transform.DOLocalMove(Pos, .1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

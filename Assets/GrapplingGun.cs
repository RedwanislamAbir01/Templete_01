using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    public Transform grapplePoint;
    public Transform gunTip;
    public float speed = 8;
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
    void Start()
    {
        
    }

    private void Update()
    {
       // currentGrapplePosition = gunTip.position;
    }
    void LateUpdate()
    {
       
        DrawRope();
    }
    private Vector3 currentGrapplePosition;
    void DrawRope()
    {
 

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint.position, Time.deltaTime *speed);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }
}

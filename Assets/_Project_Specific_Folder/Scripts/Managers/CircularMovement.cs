using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public float RotationSpeed = 1.3f;
    public Collsion c;
    private void Start()
    {
     
    }
    // Update is called once per frame
    void Update()
    {
        if (!c.Flying)
        {

#if UNITY_EDITOR
            if (Input.GetMouseButton(0) && !GameManager.Instance.GameOver && !GameManager.Instance.GameEnd)
            {
                transform.Rotate(0, (Input.GetAxis("Mouse X") * RotationSpeed * 1000 * Time.deltaTime), 0, Space.World);
            }
#endif

#if UNITY_ANDROID
            TouchControl();
#endif
        }

    }

    private void TouchControl()
    {
        float pointer_x = Input.GetAxis("Mouse X");
        if (Input.touchCount > 0)
        {
            pointer_x = Input.touches[0].deltaPosition.x;
            transform.Rotate(0, (pointer_x * 15 * Time.deltaTime), 0, Space.World);
        }
    }
}

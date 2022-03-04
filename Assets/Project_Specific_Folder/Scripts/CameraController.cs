using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SharkAttack
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance;
        public Transform target;
        public Vector3 offset;
        public float smoothFactor;

        private bool isTargetFound = false;
        private Vector3 defaultPos;
        private Vector3 step;

        private CanvasScaler playerCanvasScaler;
        private Camera mainCam;
     


        public static CameraController SharedManager()
        {
            return Instance;
        }


        private void Awake()
        {
            if (!Instance)
                Instance = this;
          
        }

        private void Start()
        {
            mainCam = GetComponent<Camera>();
            defaultPos = transform.localPosition;
       
   
           
        }


        private void LateUpdate()
        {
 
            if (target == null)
            {
                return;
            }

            if (target && isTargetFound == false)
            {

                isTargetFound = true;
            }

            Vector3 desiredPosition = target.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(desiredPosition, transform.position, smoothFactor * Time.deltaTime);
            transform.position = smoothedPosition;
            transform.LookAt(target);
        }




        public void ShakeCamera()
        {
            transform.DOShakePosition(1f, new Vector3(100f, 50f, 0), 15, 10f, false, true);
        }

        public void BlastShake()
        {
            transform.DOShakePosition(1f, new Vector3(100f, 50f, 0), 10, 8f, false, true);
        }
    }
}
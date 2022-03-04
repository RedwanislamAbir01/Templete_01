using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RunnerControl : MonoBehaviour
{
    public enum State
    {
        left,
        right,
        middle
    }
    private Vector3 lastPosition;
    public State direction;
    [Header("Control & Movement")]
    private Rigidbody playerRigidbody;
    private Vector3 deviation;
    public float maxDragDistance = 40f;
    private Vector3 moveDirection = Vector3.forward;
    private Vector3 currentDirection = Vector3.forward;
    public float sensitivity = 300f;
    public float SwipeSensitivity = 1f;
    public float turnTreshold = 15f;
    private Vector3 mouseStartPosition;
    private Vector3 mouseCurrentPosition;
    protected Quaternion targetRotation;
    [SerializeField] private float movementSmoothing;
    [SerializeField] private float rotationSmoothing;
    public Touch initTouch = new Touch();
    public Vector3 originPos;
    public float positionX, positionY;
    public float speed = 0.5f, computerSpeed, dir = -1f;
    public float rotationSpeed;
    public float mapWidth = 2f;
    public bool touching = false;
    private Rigidbody rb;
    private Vector3 mouseCurrentPos;

    public float FowradSpeed;
    void Start()
    {
        
        turnTreshold = 50;
        rb = GetComponent<Rigidbody>();
        positionX = transform.localPosition.x;
        positionY = transform.localPosition.y;
        originPos = this.transform.position;
        
        speed = .3f;
      
        direction = State.middle;
        playerRigidbody = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }
    private void LateUpdate()
    {
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 70, 1f);
    }
    private void Update()
    {
       
            transform.position += moveDirection * .5f * FowradSpeed * Time.deltaTime;

            HandlePlayerMovement();
        
    }



    public void HandlePlayerMovement()
    {
        mouseCurrentPos = Input.mousePosition;
        if (Input.GetAxis("Mouse X") > .1f)
        {
            direction = State.right;
            //  CamParent.transform.GetChild(0).transform.DORotate(new Vector3(0,0, -3), .3f);
        }
        if (Input.GetAxis("Mouse X") < -.1f)
        {

            direction = State.left;
            // CamParent.transform.GetChild(0).transform.DORotate(new Vector3(0, 0, +3), .3f);
        }
        if (Input.GetAxis("Mouse X") == 0)
        {

            direction = State.middle;
            // CamParent.transform.GetChild(0).transform.DORotate(new Vector3(0, 0, 0), 0f);
        }


        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)        //if finger touches the screen
            {
                if (touching == false)
                {
                    touching = true;
                    initTouch = touch;
                }
            }
            else if (touch.phase == TouchPhase.Moved)       //if finger moves while touching the screen
            {

                float deltaX = initTouch.position.x - touch.position.x;
                positionX -= (deltaX / (float)Screen.width) / Time.deltaTime * speed * dir;
                positionX = Mathf.Clamp(positionX, -3, 3);      //to set the boundaries of the player's position
                transform.localPosition = new Vector3(-positionX, positionY, transform.localPosition.z);
                initTouch = touch;

                if (positionX < 0)
                {

                }
                else if (positionX > 0)
                {

                }
                else
                {

                }
            }
            else if (touch.phase == TouchPhase.Ended)       //if finger releases the screen
            {
                initTouch = new Touch();
                touching = false;

            }
        }

        //if you play on computer---------------------------------
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * computerSpeed;     //you can move by pressing 'a' - 'd' or the arrow keys

        if (x < 0)
        {
         //   Camera.main.transform.DOLocalRotate(new Vector3(0, 0, -2), .3f);
        }
        else if (x > 0)
        {
         //   Camera.main.transform.DOLocalRotate(new Vector3(0, 0, 2), .3f);
        }
        else
        {
        //    Camera.main.transform.DOLocalRotate(new Vector3(0, 0, 0), .3f);
        }
        Vector3 newPosition = rb.transform.localPosition + Vector3.right * x;
        newPosition.x = Mathf.Clamp(newPosition.x, -3, 3);
        transform.localPosition = newPosition;
        //--------------------------------------------------------



    }

    // Update is called once per frame
}

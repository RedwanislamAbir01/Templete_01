using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NewControler : MonoBehaviour
{

    public enum PlayerType
    {
        Human,
        Bot
    }
    public enum MovementType
    {
        Position,
        Rotation
    }



    public GameObject Target;
    [Header("Type: ")]

    public bool Bike;
    public PlayerType playerType;
    public MovementType movementType;




    [HideInInspector] public bool isDead;



    [Header("Components: ")]
    public Transform rotator;
    private Rigidbody playerRigidbody;


    [Header("Control Paraemeters: ")]

    private float? lastMousePoint = null;
   

    private float deltaMousePositionX;
    private float targetRotation;


    [Header("Movement Paraemeters: ")]
    public bool onPath;


    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float pathRotationSmoothing = 0.8f;
    [SerializeField] private float maxRotationAngle = 2.5f;
    [SerializeField] private float minRotationAngle = -2.5f;
   


    public Transform center;





    public bool simulated;

    #region MonoBehaviour Callbacks
    private void Awake()
    {

        onPath = true;


    }

    private void Start()
    {

        center = transform;

        switch (playerType)
        {
            case PlayerType.Human:
                UpdateMouseInput();

                break;

        }

        switch (movementType)
        {
            case MovementType.Position:
         
                break;
            case MovementType.Rotation:
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (rotator == null)
        {
         //   rotator = transform.GetChild(3);
        }
        switch (playerType)
        {
    
            case PlayerType.Human:

                UpdateMouseInput();
                //UpdateUI();
                break;
        }

        UpdateMovement();

    }

    private void OnDisable()
    {

    }
    #endregion

    #region Input & Player Controller
    private void UpdateMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePoint = Input.mousePosition.x;
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastMousePoint = null;
            deltaMousePositionX = 0f;
        }

        if (lastMousePoint != null)
        {
            deltaMousePositionX = Input.mousePosition.x - lastMousePoint.Value;
            lastMousePoint = Input.mousePosition.x;
        }
    }

    private void SimulateInput()
    {
        if (!simulated)
        {
            simulated = true;
            StartCoroutine(SimulateInputRoutine());
        }
    }

    IEnumerator SimulateInputRoutine()
    {
        while (true)
        {
            deltaMousePositionX = Random.Range(-8f, 8f);
            yield return new WaitForSeconds(Random.Range(1f, 2.5f));
            deltaMousePositionX = Random.Range(-10f, 0f);
            yield return new WaitForSeconds(Random.Range(1f, 2.5f));
        }
    }

    private void UpdateMovement()
    {
        if (onPath)
        {
            targetRotation += rotationSpeed * Time.deltaTime * deltaMousePositionX;

            targetRotation = Mathf.Clamp(targetRotation, minRotationAngle, maxRotationAngle);
            switch (movementType)
            {
                case MovementType.Position:
                    rotator.localPosition = Vector3.Lerp(rotator.localPosition,
                        new Vector3((-center.localPosition.x/*rotator.localPosition.x*/ + targetRotation), rotator.localPosition.y, rotator.localPosition.z), pathRotationSmoothing / 10f);
                    //   rotator.localRotation = Quaternion.Slerp(rotator.localRotation, Quaternion.Euler(0, rotator.localRotation.y + targetRotation * 5, 0f), pathRotationSmoothing);
                    if (Bike)
                    {

                        rotator.transform.DOLocalRotate(new Vector3(0, 0, rotator.localRotation.y + targetRotation * 1.2f), .2f).OnComplete(() =>
                        {



                        });
                    }
                    break;
                case MovementType.Rotation:
                    rotator.localRotation = Quaternion.Slerp(rotator.localRotation, Quaternion.Euler(0, rotator.localRotation.y + targetRotation, 0f), pathRotationSmoothing);
                    break;
                default:
                    break;
            }
        }
    }

    public float GetMouseDeltaPosition()
    {
        return deltaMousePositionX;
    }
    #endregion

    public void SetPlayerEventPrperties(float max, float min)
    {
        transform.localPosition = new Vector3(1f,
                    transform.localPosition.y, transform.localPosition.z);
        maxRotationAngle = max;
        minRotationAngle = min;
    }

}

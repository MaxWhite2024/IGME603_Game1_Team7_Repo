using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player_Movement : MonoBehaviour
{
    [Header("Player Movement Variables")] [SerializeField]
    private Rigidbody playerRigidbody;

    [SerializeField] private float moveForce = 0f;
    private bool isPlayerHorizontalMoving = false;
    private Vector3 moveDir = Vector3.zero;

    [Header("Camera Movement Variables")] [SerializeField]
    private Transform horizontalCameraPivot;

    [SerializeField] private Transform verticalCameraPivot;
    [SerializeField] private float cameraMovespeed = 0f;
    public bool isCameraXInverted;
    public bool isCameraYInverted;
    private bool isCameraHorizontalTurning = false;
    private bool isCameraVerticalTurning = false;
    private Vector3 cameraTurnDir = Vector3.zero;
    private float oldCameraFOV;
    [SerializeField] private float targetCameraFOV;
    //[SerializeField] private float cameraDistanceChangeSpeed = 1f;
    [SerializeField] private float ratCollectionCameraChangeAmount = 1f;
    [SerializeField] private float maxFOV = 80f;

    [Header("Player Jump Varaibles")] 
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float groundCheckBoxVerticalOffset = 0f;
    [SerializeField] private float groundCheckBoxSize = 1f;
    private bool isGroundDetected;
    public bool canControlPlayer = true;

    private void Start()
    {
        targetCameraFOV = oldCameraFOV = Camera.main.fieldOfView;
        //targetCameraDistance = Vector3.Distance(Camera.main.gameObject.transform.position, gameObject.transform.position);
        //oldTargetCameraDistance = targetCameraDistance;
    }

    private void Update()
    {
        //calculate direction from player to camera
        //Vector3 direction = Camera.main.gameObject.transform.position - gameObject.transform.position;
        //Camera.main.gameObject.transform.position = Vector3.Lerp(direction * oldTargetCameraDistance, direction * targetCameraDistance, Time.deltaTime);
        if(Camera.main.fieldOfView <= maxFOV)
            Camera.main.fieldOfView = Mathf.Lerp(oldCameraFOV, targetCameraFOV, Time.deltaTime);
        else
            Camera.main.fieldOfView = maxFOV;
    }
   

    private void FixedUpdate()
    {
        //Debug.Log(verticalCameraPivot.localEulerAngles.x);
        //Debug.Log(horizontalPivot.forward);
        //if player is inputting a player movement input,...
        if (isPlayerHorizontalMoving)
        {
            //get player input
            float playerVerticalInput = moveDir.z;
            float playerHorizontalInput = moveDir.x;

            //get horizontal pivot direction
            Vector3 forward = horizontalCameraPivot.forward;
            Vector3 right = horizontalCameraPivot.right;

            //remove upwards components from forward and right
            forward.y = 0f;
            right.y = 0f;

            //normalize forward and right (MAYBE UNNECESSARY???)
            forward = forward.normalized;
            right = right.normalized;

            //create pivot-direction-relative-input vectors
            Vector3 forwardRelativeHorizontalInput = playerVerticalInput * forward;
            Vector3 rightRelativeHorizontalInput = playerHorizontalInput * right;

            //calculate pivotRelativeMovement
            Vector3 pivotRelativeMovement = forwardRelativeHorizontalInput + rightRelativeHorizontalInput;

            if (canControlPlayer)
            {
                //apply moveForce force to the player in the horizontal direction the camera is facing
                playerRigidbody.AddForce(pivotRelativeMovement * moveForce, ForceMode.Force);
            }
        }

        //if player inputting a horizontal camera turning input,...
        if (isCameraHorizontalTurning)
        {
            //rotate the horizontal camera pivot point by cameraTurnDir.x
            horizontalCameraPivot.Rotate(0f, cameraTurnDir.x * cameraMovespeed, 0f);
        }

        //if player is inputting a vertical camera  turning input,...
        if (isCameraVerticalTurning)
        {
            verticalCameraPivot.Rotate(cameraTurnDir.y * cameraMovespeed, 0f, 0f);
            verticalCameraPivot.eulerAngles = new Vector3(verticalCameraPivot.eulerAngles.x, Mathf.Clamp(transform.eulerAngles.y, -89.8f, 89.9f), 0f);
        }
    }

    void OnMove(InputValue value)
    {
        //if player started moving the player horizontally,...
        if (value.Get<Vector2>() != Vector2.zero)
        {
            isPlayerHorizontalMoving = true;
            moveDir.z = value.Get<Vector2>().y;
            moveDir.x = value.Get<Vector2>().x;
        }
        //else player stopped moving the player horizontally,...
        else
        {
            isPlayerHorizontalMoving = false;
            moveDir = Vector2.zero;
        }
    }

    void OnCameraMove(InputValue value)
    {
        //Debug.Log(value.Get<Vector2>());
        //if player started turning the camera horizontally,...
        if (value.Get<Vector2>().x != 0f)
        {
            isCameraHorizontalTurning = true;
            cameraTurnDir.x = value.Get<Vector2>().x * (isCameraXInverted ? -1f : 1f);
        }
        //else player stopped turning the camera horizontally,...
        else
        {
            isCameraHorizontalTurning = false;
            cameraTurnDir.x = 0f;
        }

        //if player started turning the camera vertically,...
        if (value.Get<Vector2>().y != 0f)
        {
            isCameraVerticalTurning = true;
            cameraTurnDir.y = value.Get<Vector2>().y * (isCameraYInverted ? -1f : 1f);
        }
        //else player stopped turning the camera vertically,...
        else
        {
            isCameraVerticalTurning = false;
            cameraTurnDir.y = 0f;
        }
    }

    public void ChangeCameraDistance(short changeAmount)
    {
        //if changeAmount is positive,...
        if (changeAmount >= 0)
        {
            Debug.Log(Camera.main.fieldOfView + ratCollectionCameraChangeAmount);
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView + ratCollectionCameraChangeAmount, 60f, maxFOV);
        }
        //else changeAmount is negative,...
        else
        {
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - ratCollectionCameraChangeAmount, 60f, maxFOV);
        }

        ////save old target FOV
        //oldCameraFOV = targetCameraFOV;

        ////change target distance
        //if (changeAmount >= 0)
        //    targetCameraFOV += ratCollectionCameraChangeAmount;
        //else
        //    targetCameraFOV -= ratCollectionCameraChangeAmount;
    }

    void OnJump()
    {
        if (!canControlPlayer) return;
        
        //box cast from:
        //center = gameObject.transform.position + new Vector3(0f, groundCheckBoxVerticalOffset, 0f)
        //halfExtends = Vector3.one * groundCheckBoxSize / 2f
        //orientation = Quaternion.identity
        //layerMask = LayerMask.GetMask("Ground")
        isGroundDetected =
            Physics.CheckBox(gameObject.transform.position + new Vector3(0f, groundCheckBoxVerticalOffset, 0f),
                Vector3.one * groundCheckBoxSize / 2f, Quaternion.identity, LayerMask.GetMask("Ground"));

        //if CheckBox hit something,...
        if (isGroundDetected)
        {
            //blast the player upwards by jumpForce
            playerRigidbody.AddForce(horizontalCameraPivot.up * jumpForce, ForceMode.Impulse);
        }
    }

    //draw the player's groundcheck hitbox
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gameObject.transform.position + new Vector3(0f, groundCheckBoxVerticalOffset, 0f),
            Vector3.one * groundCheckBoxSize);
    }
}
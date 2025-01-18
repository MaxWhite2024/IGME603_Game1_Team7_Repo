using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player_Movement : MonoBehaviour
{
    [Header("Player Movement Variables")]
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float moveForce = 0f;
    private bool isPlayerHorizontalMoving = false;
    private Vector3 moveDir = Vector3.zero;

    [Header("Camera Movement Variables")]
    [SerializeField] private Transform horizontalCameraPivot;
    [SerializeField] private Transform verticalCameraPivot;
    [SerializeField] private float cameraMovespeed = 0f;
    private bool isCameraHorizontalTurning = false;
    private bool isCameraVerticalTurning = false;
    private Vector3 cameraTurnDir = Vector3.zero;

    private void FixedUpdate()
    {
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

            //apply moveForce force to the player in the horizontal direction the camera is facing
            playerRigidbody.AddForce(pivotRelativeMovement * moveForce, ForceMode.Force);
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
            //rotate the vertical camera pivot point by cameraTurnDir.y clamped between -90 and 90
            verticalCameraPivot.Rotate(cameraTurnDir.y * cameraMovespeed, 0f, 0f);
        }
    }

    void OnMove(InputValue value)
    {
        //if player started moving the player horizontally,...
        if(value.Get<Vector2>() != Vector2.zero)
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
            cameraTurnDir.x = value.Get<Vector2>().x;
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
            cameraTurnDir.y = value.Get<Vector2>().y;
        }
        //else player stopped turning the camera vertically,...
        else
        {
            isCameraVerticalTurning = false;
            cameraTurnDir.y = 0f;
        }
    }
}

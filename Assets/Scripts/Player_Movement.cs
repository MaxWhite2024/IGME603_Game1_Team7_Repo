using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    //TO DO: 
    // - rename variables to camelcase
    // - rename horizontalPivot to horizontalPivot
    // - add verticalPivot gameobject

    [Header("Player Movement Variables")]
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Transform playerHorizontalTransform;
    [SerializeField] private float moveForce = 0f;
    private bool isPlayerHorizontalMoving = false;
    private Vector3 moveDir = Vector3.zero;

    [Header("Camera Movement Variables")]
    [SerializeField] private Transform horizontalPivot;
    [SerializeField] private float cameraMovespeed = 0f;
    private bool isCameraHorizontalTurning = false;
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
            Vector3 forward = horizontalPivot.forward;
            Vector3 right = horizontalPivot.right;

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

        //is player inputting a camera turning input,...
        if(isCameraHorizontalTurning)
        {
            //rotate the horizontal camera pivot point by cameraTurnDir.x
            horizontalPivot.Rotate(0f, cameraTurnDir.x, 0f);
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
        //if player started turning the camera horizontally,...
        if(value.Get<Vector2>().x != 0f)
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
    }
}

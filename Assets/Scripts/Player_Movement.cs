using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    //TO DO: 
    // - rename variables to camelcase
    // - rename horizontal_Pivot to horizontalPivot
    // - add verticalPivot gameobject

    [Header("Player Movement Variables")]
    [SerializeField] private Rigidbody player_Rigidbody;
    [SerializeField] private Transform player_Horizontal_Transform;
    [SerializeField] private float move_Force = 0f;
    private bool is_Player_Horizontal_Moving = false;
    private Vector3 move_Dir = Vector3.zero;

    [Header("Camera Movement Variables")]
    [SerializeField] private Transform horizontal_Pivot;
    [SerializeField] private float camera_Movespeed = 0f;
    private bool is_Camera_Horizontal_Turning = false;
    private Vector3 camera_Turn_Dir = Vector3.zero;

    private void FixedUpdate()
    {
        //Debug.Log(horizontal_Pivot.forward);
        //if player is inputting a player movement input,...
        if (is_Player_Horizontal_Moving)
        {
            //get player input
            float playerVerticalInput = move_Dir.z;
            float playerHorizontalInput = move_Dir.x;

            //get horizontal pivot direction
            Vector3 forward = horizontal_Pivot.forward;
            Vector3 right = horizontal_Pivot.right;

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

            //apply move_Force force to the player in the horizontal direction the camera is facing
            player_Rigidbody.AddForce(pivotRelativeMovement * move_Force, ForceMode.Force);
        }

        //is player inputting a camera turning input,...
        if(is_Camera_Horizontal_Turning)
        {
            //rotate the horizontal camera pivot point by camera_Turn_Dir.x
            horizontal_Pivot.Rotate(0f, camera_Turn_Dir.x, 0f);
        }
    }

    void OnMove(InputValue value)
    {
        //if player started moving the player horizontally,...
        if(value.Get<Vector2>() != Vector2.zero)
        {
            is_Player_Horizontal_Moving = true;
            move_Dir.z = value.Get<Vector2>().y;
            move_Dir.x = value.Get<Vector2>().x;
        }
        //else player stopped moving the player horizontally,...
        else
        {
            is_Player_Horizontal_Moving = false;
            move_Dir = Vector2.zero;
        }
    }

    void OnCameraMove(InputValue value)
    {
        //if player started turning the camera horizontally,...
        if(value.Get<Vector2>().x != 0f)
        {
            is_Camera_Horizontal_Turning = true;
            camera_Turn_Dir.x = value.Get<Vector2>().x;
        }
        //else player stopped turning the camera horizontally,...
        else
        {
            is_Camera_Horizontal_Turning = false;
            camera_Turn_Dir.x = 0f;
        }
    }
}

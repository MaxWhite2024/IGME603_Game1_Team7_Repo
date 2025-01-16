using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    [Header("Player Movement Variables")]
    [SerializeField] private Rigidbody player_Rigidbody;
    [SerializeField] private Transform player_Horizontal_Transform;
    [SerializeField] private float move_Force = 0f;
    private bool is_Player_Horizontal_Moving = false;
    private Vector3 move_Dir;

    [Header("Camera Movement Variables")]
    [SerializeField] private Transform horizontal_Pivot;
    [SerializeField] private float camera_Movespeed = 0f;

    private void FixedUpdate()
    {
        if (is_Player_Horizontal_Moving)
        {
            Debug.Log(move_Dir);

            //apply move_Force force to the player in the horizontal direction the camera is facing
            player_Rigidbody.AddForce(move_Dir * move_Force, ForceMode.Force);
            //player_Horizontal_Transform.rotation.eulerAngles
        }
    }

    void OnMove(InputValue value)
    {

        if(value.Get<Vector2>() != Vector2.zero)
        {
            is_Player_Horizontal_Moving = true;
            move_Dir.z = value.Get<Vector2>().y;
            move_Dir.x = value.Get<Vector2>().x;
        }
        else
        {
            is_Player_Horizontal_Moving = false;
            move_Dir = Vector2.zero;
        }
    }

    void OnCameraMove(InputValue value)
    {
        horizontal_Pivot.Rotate(0f, value.Get<Vector2>().x, 0f);
    }
}

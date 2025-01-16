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

    [Header("Camera Movement Variables")]
    [SerializeField] private GameObject horizontal_Pivot;
    [SerializeField] private float camera_Movespeed = 0f;

    void OnMove(InputValue value)
    {
        //calculate movement direction
        Vector3 move_Dir = Vector3.zero;
        move_Dir.z = value.Get<Vector2>().y;
        move_Dir.x = value.Get<Vector2>().x;
            //player_Horizontal_Transform.rotation.eulerAngles;

        //apply move_Force force to the player in the horizontal direction the camera is facing
        player_Rigidbody.AddForce(move_Dir * move_Force);
        //player_Horizontal_Transform.rotation.eulerAngles
    }
}

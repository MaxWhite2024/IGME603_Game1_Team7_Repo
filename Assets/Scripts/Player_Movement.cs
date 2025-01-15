using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    [Header("Player Movement Variables")]
    [SerializeField] private Rigidbody player_Rigidbody;
    [SerializeField] private float move_Force = 0f;

    [Header("Camera Movement Variables")]
    [SerializeField] private GameObject horizontal_Pivot;
    [SerializeField] private float camera_Movespeed = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMove(InputValue value)
    {
        //apply move_Force force in the horizontal direction the camera is facing to the player
        player_Rigidbody.AddForce(horizontal_Pivot.transform.position * move_Force, ForceMode.Impulse);
    }
}

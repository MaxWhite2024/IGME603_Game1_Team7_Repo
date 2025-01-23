using System;
using UnityEngine;

public class RespawnOnFallingOutOfWorld: MonoBehaviour
{
    [SerializeField] private float respawnHeight = -100f;
    
    private Vector3 _spawnPoint;
    private Rigidbody _body;

    private void Start()
    {
        _spawnPoint = transform.position;
        _body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (transform.position.y > respawnHeight) return;
        if (_body) _body.velocity = Vector3.zero;
        transform.position = _spawnPoint;
    }
}
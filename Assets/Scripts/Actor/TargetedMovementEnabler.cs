using System;
using UnityEngine;

public class TargetedMovementEnabler : MonoBehaviour
{
    [SerializeField] private TargetedChaseMovement targetedBehavior;
    [SerializeField] private RandomWanderMovement wanderBehavior;

    private void Start()
    {
        wanderBehavior.enabled = true;
        targetedBehavior.enabled = false;
    }

    private void SetTarget(Transform target)
    {
        targetedBehavior.target = target;
        
        wanderBehavior.enabled = false;
        targetedBehavior.enabled = true;
    }

    private void RemoveTarget()
    {
        wanderBehavior.enabled = true;
        targetedBehavior.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        SetTarget(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveTarget();
    }
}
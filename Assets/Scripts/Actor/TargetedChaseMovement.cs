using UnityEngine;

public class TargetedChaseMovement : MonoBehaviour
{
    public Transform target;

    [SerializeField] private Rigidbody body;
    [SerializeField] [Range(0.1f, 50f)] private float maxSteeringForce = 2f;
    [SerializeField] private float maxSpeed = 2f;
    
    private void FixedUpdate()
    {
        CalculateForces();
    }
    
    private void CalculateForces()
    {
        var desiredVelocity = (target.position - transform.position).Copy(y: 0f).normalized * maxSpeed;
        var rawSteeringForce = desiredVelocity - body.velocity;
        var steering = Vector3.ClampMagnitude(rawSteeringForce, maxSteeringForce) / body.mass;

        var resultVelocity = Vector3.ClampMagnitude(body.velocity + steering, maxSpeed);
        body.velocity = resultVelocity;
        transform.LookAt(transform.position + resultVelocity);
    }
}

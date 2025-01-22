using UnityEngine;

// Should probably refactor this to extend from TargetChaseMovement.cs
// and simply update the target point when within the corner cutting radius
public class PathSteering : MonoBehaviour
{
    [SerializeField] private Path path;
    [SerializeField] private Rigidbody body;
    [SerializeField] private float maxSpeed = 2f;
    [SerializeField] [Range(0.1f, 50f)] private float maxSteeringForce = 2f;
    [SerializeField] [Range(0.1f, 100f)] private float cornerCutting = 5f;

    private Transform _currentTarget;
    private Path.Iterator _currentIterator;

    private void Start()
    {
        NextTarget();
    }

    private void NextTarget()
    {
        _currentIterator = path.GetNextIterator(_currentIterator);
        _currentTarget = path.GetNode(_currentIterator);
    }

    private void FixedUpdate()
    {
        CheckArrivedAtTarget();
        CalculateForces();
    }

    private void CalculateForces()
    {
        var desiredVelocity = (_currentTarget.position - transform.position).Copy(y: 0f).normalized * maxSpeed;
        var rawSteeringForce = desiredVelocity - body.velocity;
        var steering = Vector3.ClampMagnitude(rawSteeringForce, maxSteeringForce) / body.mass;

        var resultVelocity = Vector3.ClampMagnitude(body.velocity + steering, maxSpeed);
        body.velocity = resultVelocity;
        transform.LookAt(transform.position + resultVelocity);
    }

    private void CheckArrivedAtTarget()
    {
        var isNearCurrentTarget = transform.position.DistanceTo2DSquared(_currentTarget.position) < cornerCutting;
        if (isNearCurrentTarget)
        {
            NextTarget();
        }
    }
}
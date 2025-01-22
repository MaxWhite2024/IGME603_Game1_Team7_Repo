using UnityEngine;
using UnityEngine.Serialization;

public class PathFollowing : MonoBehaviour
{
    [SerializeField] private Path path;
    [SerializeField] private float maxSpeed = 2f;

    [SerializeField] private float delayAtNodeDuration = 1f;

    private Transform _currentTarget;
    private Path.Iterator _currentIterator;

    private float _reachedPointTime;
    private bool _isMoving;

    private void Start()
    {
        NextTarget();
        _isMoving = true;
    }

    private void NextTarget()
    {
        _currentIterator = path.GetNextIterator(_currentIterator);
        _currentTarget = path.GetNode(_currentIterator);
        _reachedPointTime = Time.time;
        _isMoving = false;
    }

    private void FixedUpdate()
    {
        if (ShouldMove())
        {
            Move();
        }
    }

    private bool ShouldMove()
    {
        if (_isMoving) return true;
        var nextMoveTime = _reachedPointTime + delayAtNodeDuration;
        if (Time.time < nextMoveTime) return false;

        _isMoving = true;
        return true;
    }

    private void Move()
    {
        var distanceStep = maxSpeed * Time.fixedDeltaTime;
        var targetVector = _currentTarget.position - transform.position;

        if (Vector3.Magnitude(targetVector) < distanceStep)
        {
            transform.position = _currentTarget.position;
            NextTarget();
            return;
        }

        var vectorOffset = Vector3.ClampMagnitude(targetVector, distanceStep);
        transform.position += vectorOffset;
    }
}
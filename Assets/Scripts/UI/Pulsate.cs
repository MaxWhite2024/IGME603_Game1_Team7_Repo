using UnityEngine;

public class Pulsate : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float cycleLengthSeconds = 3f;

    private bool _isIncreasing = true;
    private float _value = 0f;

    private void Update()
    {
        var sign = _isIncreasing ? 1f : -1f;
        var delta = sign * Time.deltaTime / (cycleLengthSeconds * 2f);

        _value += delta;
        if (_value >= 1f)
        {
            _value = 1f;
            _isIncreasing = false;
        }

        if (_value <= 0f)
        {
            _value = 0f;
            _isIncreasing = true;
        }

        var scale = curve.Evaluate(_value);
        transform.localScale = Vector3.one * scale;
    }
}
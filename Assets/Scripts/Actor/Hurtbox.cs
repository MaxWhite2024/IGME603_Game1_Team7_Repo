using UnityEngine;
using UnityEngine.Events;

public class Hurtbox : MonoBehaviour
{
    public UnityEvent<float> onDamageTaken;

    public void TakeDamage(float amount)
    {
        onDamageTaken?.Invoke(amount);
    }
}
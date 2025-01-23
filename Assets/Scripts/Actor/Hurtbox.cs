using UnityEngine;
using UnityEngine.Events;

public class Hurtbox : MonoBehaviour
{
    public UnityEvent<int, Vector3> onDamageTaken;

    public void TakeDamage(int amount, Vector3 position)
    {
        onDamageTaken?.Invoke(amount, position);
    }
}
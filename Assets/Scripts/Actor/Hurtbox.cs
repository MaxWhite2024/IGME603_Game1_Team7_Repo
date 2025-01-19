using UnityEngine;
using UnityEngine.Events;

public class Hurtbox : MonoBehaviour
{
    public UnityEvent<int> onDamageTaken;

    public void TakeDamage(int amount)
    {
        onDamageTaken?.Invoke(amount);
    }
}
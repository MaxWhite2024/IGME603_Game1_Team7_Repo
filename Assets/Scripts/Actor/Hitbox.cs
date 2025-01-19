using System;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        var hurtbox = other.GetComponent<Hurtbox>();
        if (!hurtbox) return;
        
        Debug.Log($"Trigger enter {name} -> {other.name}");
        
        hurtbox.TakeDamage(damage);
    }
}

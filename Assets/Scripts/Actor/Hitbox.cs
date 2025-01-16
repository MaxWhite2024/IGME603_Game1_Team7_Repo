using System;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private float damage = 1f;

    private void OnTriggerEnter(Collider other)
    {
        var hurtbox = other.GetComponent<Hurtbox>();
        if (!hurtbox) return;
        
        hurtbox.TakeDamage(damage);
    }
}

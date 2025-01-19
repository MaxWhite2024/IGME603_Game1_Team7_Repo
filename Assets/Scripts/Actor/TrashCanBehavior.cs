using System;
using UnityEngine;

public class TrashCanBehavior : MonoBehaviour
{
    private static readonly int WobbleId = Animator.StringToHash("Wobble");
    private static readonly int TippedId = Animator.StringToHash("IsTipped");

    [SerializeField] private int requirement = 0;
    [SerializeField] private Animator visual;

    [SerializeField] private GameObject itemPrefab;

    private bool _isTipped = false;

    public void AttemptTip(int damage)
    {
        if (_isTipped) return;

        Debug.Log($"Tip attempted {damage}");
        if (damage >= requirement)
        {
            Tip();
        }
        else
        {
            Wobble();
        }
    }

    private void Tip()
    {
        Debug.Log("Tipping");
        _isTipped = true;

        if (itemPrefab) SpawnItem();
        if (visual) visual.SetBool(TippedId, true);
    }

    private void Wobble()
    {
        Debug.Log("Wobbling");
        if (visual) visual.SetTrigger(WobbleId);
    }

    private void SpawnItem()
    {
        Instantiate(itemPrefab, transform.position + transform.forward, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position + Vector3.up * 0.2f, transform.forward);
    }
}
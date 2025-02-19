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

    public void AttemptTip(int damage, Vector3 position)
    {
        if (_isTipped) return;
        var target = position.Copy(y: transform.position.y);
        transform.rotation = Quaternion.LookRotation(transform.position - target);

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
        var spawnedItem = Instantiate(original: itemPrefab);
        spawnedItem.transform.position = transform.position + transform.forward * 2f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position + Vector3.up * 0.2f, transform.forward);
    }
}
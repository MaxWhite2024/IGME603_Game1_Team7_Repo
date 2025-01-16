using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private Color debugColor = Color.red;
    
    public List<Transform> nodes = new();

    private void OnDrawGizmos()
    {
        Gizmos.color = debugColor;
        for (int i = 1; i < nodes.Count; i++)
        {
            Gizmos.DrawWireSphere(nodes[i].position, 0.5f);
            Gizmos.DrawLine(nodes[i - 1].position, nodes[i].position);
        }

        if (nodes.Count >= 3)
        {
            Gizmos.DrawWireSphere(nodes[0].position, 0.5f);
            Gizmos.DrawLine(nodes.Last().position, nodes[0].position);
        }
    }
}
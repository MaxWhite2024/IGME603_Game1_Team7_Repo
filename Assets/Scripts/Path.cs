using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
    private enum Mode
    {
        Loop,
        PingPong
    }

    [SerializeField] private Color debugColor = Color.red;
    [SerializeField] private Mode mode = Mode.Loop;

    [SerializeField] private List<Transform> nodes = new();

    public class Iterator
    {
        internal int Index = 0;
        internal int Increment = 1;
    }

    public Transform GetNode(Iterator current)
    {
        return current == null ? null : nodes[current.Index];
    }

    public Iterator GetNextIterator(Iterator current)
    {
        var nextIterator = new Iterator
        {
            Index = current != null ? current.Index + current.Increment : 0,
            Increment = current?.Increment ?? 1
        };

        if (nextIterator.Index < 0)
        {
            switch (mode)
            {
                case Mode.Loop:
                    nextIterator.Index = nodes.Count - 1;
                    break;
                case Mode.PingPong:
                    nextIterator.Increment *= -1;
                    nextIterator.Index = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        if (nextIterator.Index >= nodes.Count)
        {
            switch (mode)
            {
                case Mode.Loop:
                    nextIterator.Index = 0;
                    break;
                case Mode.PingPong:
                    nextIterator.Increment *= -1;
                    nextIterator.Index = nodes.Count - 2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        if (nextIterator.Index < 0 || nextIterator.Index >= nodes.Count)
        {
            return null;
        }
        
        return nextIterator;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = debugColor;
        for (int i = 1; i < nodes.Count; i++)
        {
            Gizmos.DrawWireSphere(nodes[i].position, 0.5f);
            Gizmos.DrawLine(nodes[i - 1].position, nodes[i].position);
        }

        if (nodes.Count >= 1)
        {
            Gizmos.DrawWireSphere(nodes[0].position, 0.5f);
        }

        if (nodes.Count >= 3 && mode == Mode.Loop)
        {
            Gizmos.DrawLine(nodes.Last().position, nodes[0].position);
        }
    }
}
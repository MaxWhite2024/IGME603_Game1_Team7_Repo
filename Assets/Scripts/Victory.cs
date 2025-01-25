using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{

    [SerializeField] private GameObject victoryMessage;
    [SerializeField] private float timer;
    [SerializeField] private bool count;

    // Start is called before the first frame update
    void Start()
    {
        victoryMessage.SetActive(false);
        count = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(count)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                count = false;
                victoryMessage.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        CollisionLogic(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        CollisionLogic(other);
    }

    private void CollisionLogic(Collider otherCollider)
    {
        count = true;
        victoryMessage.SetActive(true);
    }
}

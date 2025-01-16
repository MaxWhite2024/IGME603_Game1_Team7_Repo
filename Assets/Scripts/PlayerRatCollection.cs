using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRatCollection : MonoBehaviour
{
    public short ratCount;
    public Vector3 ratScaleAmount;
    [SerializeField] private Vector3 scale;

    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CollisionLogic(Collider collider)
    {
        Rat rat = collider.GetComponent<Rat>();
        if (rat != null)
        {
            //Debug.Log("Rat is here");
            ratCount += rat.ratCount;
            Destroy(rat.gameObject);

            scale = new Vector3(scale.x + ratScaleAmount.x, scale.y + ratScaleAmount.y, scale.z + ratScaleAmount.z);
            transform.localScale = scale;
            return;
        }

        //Not colliding with a rat
        RatChecker checker = collider.GetComponent<RatChecker>();
        if (checker != null)
        {
            if (ratCount >= checker.ratCountNeeded)
            {
                if (checker is FireHydrant fireHydrant)
                {
                    fireHydrant.EnoughRats();
                }
                checker.EnoughRats();
            }
            else
            {
                Debug.Log("You need more rats");
            }
            return;
        }

        BouncyThings bouncy = collider.GetComponent<BouncyThings>();
        if(bouncy != null)
        {
            Vector3 newVelocity = gameObject.GetComponent<Rigidbody>().velocity;
            newVelocity.y = newVelocity.y * bouncy.bounceDampener;
            Debug.Log(newVelocity.y);

            gameObject.GetComponent<Rigidbody>().velocity = newVelocity;
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
}

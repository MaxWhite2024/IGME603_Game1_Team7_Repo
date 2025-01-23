using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerRatCollection : MonoBehaviour
{
    public short ratCount;
    public float ratScaleAmount;
    [SerializeField] private Hitbox collectiveHitbox;
    [SerializeField] private SphereCollider colliderToScale;

    // Start is called before the first frame update
    void Start()
    {
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
            OnRatCountChange(rat.ratCount);
            if (collectiveHitbox) collectiveHitbox.damage = ratCount;
            collider.transform.parent = transform;
            collider.enabled = false;
            colliderToScale.radius += ratScaleAmount;

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
        if (bouncy != null)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false; //Disables gravity so you don't fall down

            if (gameObject.GetComponent<Rigidbody>().velocity.y > 0)
            {
                return;
            }

            //This is part of the super cursed don't fall through the bounce code. Sets velocity to 0 when it's too low so you don't forever bounce
            if (gameObject.GetComponent<Rigidbody>().velocity.y >= (0 - 1.5f) &&
                gameObject.GetComponent<Rigidbody>().velocity.y <= (0 + 1.5f))
            {
                Vector3 zeroVelocity = gameObject.GetComponent<Rigidbody>().velocity;
                zeroVelocity.y = 0;
                gameObject.GetComponent<Rigidbody>().velocity = zeroVelocity;
                return;
            }

            Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);
            Vector3 newVelocity = gameObject.GetComponent<Rigidbody>().velocity;
            newVelocity.y = newVelocity.y * bouncy.bounceDampener;
            //Debug.Log(newVelocity.y);

            gameObject.GetComponent<Rigidbody>().velocity = newVelocity;
            Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);
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

    private void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true; //Enables gravity when you exit bouncy thing
    }

    private void OnRatCountChange(short changeAmount)
    {
        //change camera distance
        gameObject.GetComponent<Player_Movement>().ChangeCameraDistance(changeAmount);

        //increase rat count
        ratCount += changeAmount;
    }
}
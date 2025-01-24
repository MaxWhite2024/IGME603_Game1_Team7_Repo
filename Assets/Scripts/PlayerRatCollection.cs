using UnityEngine;

public class PlayerRatCollection : MonoBehaviour
{
    public short ratCount;
    public float ratScaleAmount;
    [SerializeField] private Hitbox collectiveHitbox;

    private SphereCollider[] _ratBallColliders;
    private Rigidbody _rigidbody;
    private Player_Movement _movement;
    private bool _isImmune = false;

    private void Start()
    {
        _movement = GetComponent<Player_Movement>();
        _ratBallColliders = GetComponents<SphereCollider>();
        _rigidbody = GetComponent<Rigidbody>();
    }


    private void CollisionLogic(Collider otherCollider)
    {
        Rat rat = otherCollider.GetComponent<Rat>();
        if (rat != null)
        {
            //Debug.Log("Rat is here");
            if (ratCount == 0) _movement.canControlPlayer = true;
            ratCount += rat.ratCount;
            if (collectiveHitbox) collectiveHitbox.damage = ratCount;
            otherCollider.transform.parent = transform;
            otherCollider.enabled = false;
            foreach (var ratCollider in _ratBallColliders)
            {
                ratCollider.radius += ratScaleAmount;
            }

            return;
        }

        //Not colliding with a rat
        RatChecker checker = otherCollider.GetComponent<RatChecker>();
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

        BouncyThings bouncy = otherCollider.GetComponent<BouncyThings>();
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

    public void TakeDamage(int damage, Vector3 position)
    {
        if (_isImmune) return;
        _isImmune = true;
        StartCoroutine(Util.AfterDelay(0.5f, () => { _isImmune = false; }));

        var direction = (transform.position - position).normalized;
        _rigidbody.AddForce(direction * damage * 500f);

        DropRat();
    }

    private void DropRat()
    {
        if (transform.childCount == 0) return;
        var ratToDrop = transform.GetChild(transform.childCount - 1);

        var ratCollision = ratToDrop.GetComponent<Collider>();
        var ratData = ratToDrop.GetComponent<Rat>();

        ratToDrop.parent = null;
        ratToDrop.transform.position = ratToDrop.transform.position.Copy(y: transform.position.y);

        ratCount -= ratData.ratCount;
        if (ratCount == 0) _movement.canControlPlayer = false;
        foreach (var ratCollider in _ratBallColliders)
        {
            ratCollider.radius -= ratScaleAmount;
        }

        StartCoroutine(Util.AfterDelay(1f, () => { ratCollision.enabled = true; }));
    }
}
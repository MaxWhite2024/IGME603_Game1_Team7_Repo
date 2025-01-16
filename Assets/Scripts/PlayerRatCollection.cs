using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRatCollection : MonoBehaviour
{
    public short rats;
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
        if(rat == null)
            return;
        
        //Debug.Log("Rat is here");
        rats += rat.ratCount;
        Destroy(rat.gameObject);

        scale = new Vector3(scale.x + ratScaleAmount.x, scale.y + ratScaleAmount.y, scale.z + ratScaleAmount.z);
        transform.localScale = scale;
        
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

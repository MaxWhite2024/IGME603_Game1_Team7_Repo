using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHydrant : RatChecker
{
    private bool broken = false;
    [SerializeField] float upwardsMomentum;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("PlayerBall");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    new public void EnoughRats()
    {
        Debug.Log("This is a hydrant");
        if(broken)
        {
            Water();
        }
        //Debug.Log("You win!");
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        broken = true;
    }

    private void Water()
    {
        Vector3 velocity = player.GetComponent<Rigidbody>().velocity;
        velocity.y += upwardsMomentum;
        player.GetComponent<Rigidbody>().velocity = velocity;
    }
}

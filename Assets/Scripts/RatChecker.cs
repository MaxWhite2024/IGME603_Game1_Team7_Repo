using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatChecker : MonoBehaviour
{
    public short ratCountNeeded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void EnoughRats()
    {
        //Debug.Log("You win!");
        Destroy(gameObject);
    }
}

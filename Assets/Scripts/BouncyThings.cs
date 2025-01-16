using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyThings : MonoBehaviour
{

    public float bounceDampener;

    // Start is called before the first frame update
    void Start()
    {
        if(bounceDampener >= 0)
        {
            bounceDampener = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

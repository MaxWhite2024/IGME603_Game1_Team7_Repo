using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatChecker : MonoBehaviour
{
    public int ratCountNeeded;
    public int nextRatMilestone;
    [SerializeField] private RatCountUI ratCountUI;

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
        ratCountUI.ChangeRequirement(nextRatMilestone);
        Destroy(gameObject);
    }
}

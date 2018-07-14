using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyClawInteraction : MonoBehaviour {

    public Rigidbody rbody;
    public bool captured = false;

    protected virtual void InClaw(GameObject obj)
    {

    }

    protected virtual void OnTriggerEnter(Collider col)
    {
        if(col.tag == "claw")
        {
            InClaw(col.gameObject);
        }
    }

    protected virtual void OnTriggerExit(Collider col)
    {
        if (col.tag == "claw")
        {
            OutClaw();
        }
    }

    protected virtual void OutClaw()
    {

    }
 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestToy : ToyClawInteraction {
 
    private void Awake()
    {
        rbody = GetComponent<Rigidbody>();
    }
    protected override void InClaw(GameObject obj)
    {
        base.InClaw(obj);
        GrabberControl.instance.ClawEnter(gameObject, this, rbody);
    }

    protected override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        print("in the toy");
    }

    protected override void OnTriggerExit(Collider col)
    {
        base.OnTriggerExit(col);
        
    }

    protected override void OutClaw()
    {
        base.OutClaw();
    }

    private void Update()
    {
        
    }
}

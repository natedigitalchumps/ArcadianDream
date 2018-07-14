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
        obj.GetComponent<ClawPowers>().grabberRef.inToy = true;
        captured = true;
    }

    protected override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
      
    }

    protected override void OnTriggerExit(Collider col)
    {
        base.OnTriggerExit(col);
        rbody.constraints = RigidbodyConstraints.None;
    }

    protected override void OutClaw()
    {
        base.OutClaw();
    }
}

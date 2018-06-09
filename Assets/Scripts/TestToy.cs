using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestToy : ToyClawInteraction {


    protected override void InClaw(GameObject obj)
    {
        base.InClaw(obj);
        obj.GetComponent<ClawPowers>().grabberRef.inToy = true;
    }

    protected override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        print("in toy" + transform.name);
    }
}

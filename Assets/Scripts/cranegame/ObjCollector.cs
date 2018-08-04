using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCollector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        ToyClawInteraction interact = col.GetComponent<ToyClawInteraction>();
        if(col.tag == "toy" && interact.captured)
        {
            Destroy(col.gameObject);
            GamePlayManager.instance.IncreaseScore();
        }
    }
}

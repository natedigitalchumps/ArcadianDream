using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawPowers : MonoBehaviour {

   public GrabberControl grabberRef;


    private void Awake()
    {
        grabberRef = transform.parent.GetComponent<GrabberControl>();
        
    }


    private void OnTriggerEnter(Collider col)
    {
        grabberRef.ClawEnter(col.gameObject);
       
    }
    
}

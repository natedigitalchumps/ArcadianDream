using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawPowers : MonoBehaviour {

    public enum grabstate {empty,full};
    public grabstate clawGrabState;
    float Grabvalue;
    public float GrabValueLimit = .6f;
    GameObject GrabbedOjbect;
    GrabberControl grabberRef;
    // Use this for initialization
    private void Awake()
    {
        grabberRef = GetComponent<GrabberControl>();
    }

    // Update is called once per frame
    void Update () {
		


	}

    private void OnCollisionEnter(Collision col)
    {

    }
    
}

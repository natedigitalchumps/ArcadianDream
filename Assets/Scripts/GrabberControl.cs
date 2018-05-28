using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberControl : MonoBehaviour {

    public enum CraneState { Top, Moving, Floor };
    public CraneState CraneLocation;

    public enum grabstate { empty, full };
    public grabstate clawGrabState;

    //grabbing a object
    float Grabvalue;
    public float GrabValueLimit = .6f;
    GameObject GrabbedOjbect;
    //
    float speed = 2f;
    //rigid body
    public Rigidbody rbody;
    //come back up
    bool doUP = false;
    //final point
    public Transform point;
    
    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.name == "floor" || col.collider.tag == "toy")
        {
            CraneLocation = CraneState.Floor;

        }

        if (col.collider.tag == "toy" && clawGrabState == grabstate.empty)
        {
            Grabvalue = Random.value;
            print(Grabvalue);
            if (Grabvalue > GrabValueLimit && clawGrabState == grabstate.empty)
            {
                clawGrabState = grabstate.full;
                GrabbedOjbect = col.gameObject;
                GrabbedOjbect.transform.parent = transform;
                Rigidbody ToyRBody = GrabbedOjbect.GetComponent<Rigidbody>();
                ToyRBody.isKinematic = true;
                rbody.isKinematic = true;
            }
        }


    }


    private void Awake()
    {
 
        rbody = GetComponent<Rigidbody>();
  
    }

    private void Update()
    {


        if (Input.GetMouseButtonDown(0) && clawGrabState == grabstate.full)
        {
            GrabbedOjbect.transform.parent = null;
            Rigidbody rbody = GrabbedOjbect.GetComponent<Rigidbody>();
            rbody.isKinematic = false;
            clawGrabState = grabstate.empty;
        }


        if (Input.GetKeyDown(KeyCode.Space) && CraneLocation == CraneState.Top)
        {

            rbody.useGravity = true;
            CraneLocation = CraneState.Moving;
        }

        if (Input.GetKeyDown(KeyCode.Space) && CraneLocation == CraneState.Floor)
        {
            rbody.useGravity = false;
            CraneLocation = CraneState.Moving;
            doUP = true;
        }

        if (doUP)
        {

            float step = speed * Time.deltaTime;
            float dis = Vector3.Distance(transform.position, point.position);
            print(dis);
            if(dis>.001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, point.position, step);
            }else
            {
                doUP = false;
                CraneLocation = CraneState.Top;
            }
        }

    }
}

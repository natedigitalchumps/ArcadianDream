using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberControl : MonoBehaviour {
    public Transform ClawCenter;
    public enum CraneState { Top, Moving, Floor };
    public CraneState CraneLocation;

    public enum grabstate { empty, full };
    public grabstate clawGrabState;

    //grabbing a object
    float Grabvalue;
    public float GrabValueLimit = .6f;
    public GameObject GrabbedOjbect;
    //
    float speed = 2f;
    public static GrabberControl instance;
    //go down
    bool doDown = false;
    //come back up
     bool doUP = false;
    //top location
    public Transform point;
    public LayerMask lmask;
    public bool inToy = false;
    
    private void Awake()
    {
        instance = this;
        ClawCenter = transform.GetChild(0).transform;
    }

    public void ClawEnter(GameObject obj, ToyClawInteraction toy,Rigidbody rbody)
    {
        inToy = true;
        print("claw enter script works");
        if(GrabbedOjbect == null)
        {
  
            if (obj.CompareTag("toy") && clawGrabState == grabstate.empty)
            {
                Grabvalue = Random.value;
                //print(Grabvalue);
                if (Grabvalue > GrabValueLimit)
                {
                    GrabbedOjbect = obj;
                    GrabbedOjbect.transform.position = ClawCenter.position;
                    GrabbedOjbect.transform.parent = transform;
                    
                   
                    rbody = GrabbedOjbect.GetComponent<Rigidbody>();
                    rbody.isKinematic = true;
                    toy.captured = true;
                    clawGrabState = grabstate.full;
                }
            }
        }
    }

 

    IEnumerator smallwait()
    {

        yield return new WaitForSeconds(.02f);
        GrabbedOjbect = null;
    }


    public void LetGoFull()
    {
      if(clawGrabState == grabstate.full && ClawMovement.clawmove.AboveHole)
        {
            Rigidbody Toyrbody = GrabbedOjbect.GetComponent<Rigidbody>();
         
            Toyrbody.isKinematic = false;
            GrabbedOjbect.transform.parent = null;
            clawGrabState = grabstate.empty;
            GrabbedOjbect = null;
        }

       
    }


    void QuickDebug()
    {
        print("//");
        print("going down: " + doDown);
        print("going up: " + doUP);
        print("claw grab state: "+clawGrabState);


        print("//");
    }

    private void Update()
    {
      
            if (Input.GetMouseButtonDown(0))
            {
                LetGoFull();
            }

        if (doDown)
        {
            GoingDOWN();
        }else

        if (doUP)
        {
            GoingUP();
        }

        switch (CraneLocation)
        {
            case CraneState.Top:
                if (Input.GetKeyDown(KeyCode.Space) && clawGrabState == grabstate.empty)
                {
                    CraneLocation = CraneState.Moving;
                    doDown = true;
                }
                break;
            case CraneState.Floor:
                if (Input.GetKeyDown(KeyCode.Space))
                {

                    CraneLocation = CraneState.Moving;
                    doUP = true;
                }
                break;
        }
    }

    public void VRCraneLocation(bool trig)
    {
        switch (CraneLocation)
        {
            case CraneState.Top:
                if ( trig && clawGrabState == grabstate.empty && !ClawMovement.clawmove.AboveHole)
                {
                    CraneLocation = CraneState.Moving;
                    doDown = true;
                }
                break;
            case CraneState.Floor:
                if (trig && !ClawMovement.clawmove.AboveHole)
                {

                    CraneLocation = CraneState.Moving;
                    doUP = true;
                }
                break;
        }
    }

    public void GoingUP()
    {
        float step = speed * Time.deltaTime;
        float dis = Vector3.Distance(transform.position, point.position);

        inToy = false;


        if (dis > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, point.position, step);
        }
        else
        {
            doUP = false;
            CraneLocation = CraneState.Top;
        }
    }

    public void GoingDOWN()
    {
        RaycastHit hit;

        if (Physics.Raycast(point.position, point.transform.up, out hit, Mathf.Infinity, lmask))
        {
            float step = Time.deltaTime * speed;

            if (hit.transform.CompareTag("area"))
            {
                float dis = Vector3.Distance(transform.position, hit.point);

                if (dis > .08f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, hit.point, step);
                }
                else
                {

                    doDown = false;
                    CraneLocation = CraneState.Floor;
                }
            }
            else if(hit.transform.CompareTag("toy"))
            {
                if (!inToy)
                {
                    transform.position = Vector3.MoveTowards(transform.position, hit.point, step);
                }
                else
                {

                    doDown = false;
                    CraneLocation = CraneState.Floor;
                }
            }
        }
    }
}

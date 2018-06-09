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
        ClawCenter = transform.GetChild(0).transform;
    }

    public void ClawEnter(GameObject obj)
    {
        if(GrabbedOjbect == null)
        {
  
            if (obj.transform.tag == "toy" && clawGrabState == grabstate.empty)
            {
                Grabvalue = Random.value;
                //print(Grabvalue);
                if (Grabvalue > GrabValueLimit && clawGrabState == grabstate.empty)
                {
                    GrabbedOjbect = obj;
                    GrabbedOjbect.transform.position = ClawCenter.position;
                    GrabbedOjbect.transform.parent = transform;
                    
                   
                    Rigidbody ToyRBody = GrabbedOjbect.GetComponent<Rigidbody>();
                    ToyRBody.useGravity = false;
                    
                    clawGrabState = grabstate.full;
                }
            }
        }
    }

 

    IEnumerator smallwait()
    {

        yield return new WaitForSeconds(.5f);
        GrabbedOjbect = null;
    }

    private void Update()
    {


        if (Input.GetMouseButtonDown(0) && clawGrabState == grabstate.full)
        {
           
            Rigidbody Toyrbody = GrabbedOjbect.GetComponent<Rigidbody>();
            Toyrbody.useGravity = true;
           
            GrabbedOjbect.transform.parent = null;
            clawGrabState = grabstate.empty;
            StartCoroutine(smallwait());
        }


        switch (CraneLocation)
        {
            case CraneState.Top:
                if(Input.GetKeyDown(KeyCode.Space) && clawGrabState == grabstate.empty)
                {
                    CraneLocation = CraneState.Moving;
                    doDown = true;
                }
                break;
            case CraneState.Floor:
                if(Input.GetKeyDown(KeyCode.Space))
                {
                  
                    CraneLocation = CraneState.Moving;
                  doUP = true;
                }
                break;
        }

        if(doDown)
        {
            RaycastHit hit;

            if (Physics.Raycast(point.position, point.transform.up, out hit, Mathf.Infinity, lmask))
            {
                float step = Time.deltaTime * speed;

                if(hit.transform.tag == "area")
                {
                    float dis = Vector3.Distance(transform.position, hit.point);

                    if(dis>.01f)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, hit.point, step);
                    }
                    else
                    {

                        doDown = false;
                        CraneLocation = CraneState.Floor;
                    }
                }
                else
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


        if (doUP)
        {

            float step = speed * Time.deltaTime;
            float dis = Vector3.Distance(transform.position, point.position);

            inToy = false;

       
            if(dis>0)
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

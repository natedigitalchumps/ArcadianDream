using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClawMovement : MonoBehaviour {

   
    public float speed = 2;
    public Transform floatTarget;
    Rigidbody rbody;
    public LayerMask lmask;
    public static ClawMovement clawmove;
    public bool AboveHole = false;

    //simple line
    public Transform clawpoint;
    public Transform ceilingpoint;
    public GameObject ropeObj;


    private void Awake()
    {
        rbody = GetComponent<Rigidbody>();
        clawmove = this;
    }

    void ShortRope()
    {
        ceilingpoint.position = new Vector3(clawpoint.position.x, ceilingpoint.position.y, clawpoint.position.z);
        ropeObj.transform.position = (clawpoint.position + ceilingpoint.position) / 2;
        Vector3 scale = ropeObj.transform.localScale;
        scale.y = (ceilingpoint.position.y - clawpoint.position.y) / 2;
        ropeObj.transform.localScale = scale;
    }


    // Update is called once per frame
    void FixedUpdate () {

        if(GameManager.instance.buildplatform == GameManager.BuildPlatform.UnityEditor)
        {
            float hor;
            float front;

            hor = Input.GetAxis("Horizontal");
            front = Input.GetAxis("Vertical");

            rbody.velocity = -transform.forward * front * (speed * Time.deltaTime) + -transform.right * hor * (speed * Time.deltaTime);

        }else if(GameManager.instance.buildplatform == GameManager.BuildPlatform.Android)
        {
            VRController.instance.CraneMovement(rbody, speed,transform);
        }
        else
        {
            VRController.instance.quicktext.text = "not sure";
        }


    }

    private void Update()
    {
        ShortRope();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity, lmask))
        {
            AboveHole = false;
            floatTarget.gameObject.SetActive(true);
            Vector3 vec3 = hit.point;
            vec3.y += .02f;
            floatTarget.position = vec3;
        } else
        {
            AboveHole = true;
            floatTarget.gameObject.SetActive(false);
        }
    }
}

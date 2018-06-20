using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using UnityEngine.UI;

public class VRController : MonoBehaviour {
    public static VRController instance;
   public OVRInput.Controller controller;
    public Text quicktext;
    Vector2 touchpoint;
    void Awake()
    {
        instance = this;
    }  
 
    void Update()
    {
        OVRInput.Update();
        if(OVRInput.IsControllerConnected(controller))
        {
            
            touchpoint = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, controller);
            
        }
        else
        {
            quicktext.text = "nothing here";
        }
 
    }

    public void CraneMovement(Rigidbody rbody, float speed,Transform crane)
    {
        rbody.velocity = -crane.forward * touchpoint.y * (speed * Time.deltaTime) + -crane.right * touchpoint.x * (speed * Time.deltaTime);

    }

}

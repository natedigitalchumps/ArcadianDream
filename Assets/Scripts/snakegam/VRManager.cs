using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using UnityEngine.UI;

public class VRManager : MonoBehaviour {

    Vector2 touchPoint;
    public OVRInput.Controller controller;
    public static VRManager instance;
    public Text debugText;
    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      //  OVRInput.Update();
        controller = OVRInput.GetActiveController();

        touchPoint = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

       float vec2= Vector2.SignedAngle(Vector2.up, touchPoint.normalized);
        controllerInput(vec2);
        debugText.text = "input: " + vec2;
    }

    public void controllerInput( float vec2)
    {

    }

 
}

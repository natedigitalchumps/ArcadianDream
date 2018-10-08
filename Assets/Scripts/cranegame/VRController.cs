using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VRController : MonoBehaviour {
    public static VRController instance;
   public OVRInput.Controller controller;
    public Text quicktext;
    Vector2 touchpoint;
    bool TriggerPush = false;
    bool TouchPush =false;
    bool BackButton = false;
    void Awake()
    {
        instance = this;
        
    }  
 
    void Update()
    {
       
      //  OVRInput.Update();
        controller = OVRInput.GetActiveController();
        if (OVRInput.IsControllerConnected(controller))
        {
            touchpoint = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, controller);
            BackButton = OVRInput.GetDown(OVRInput.Button.Back);
            TriggerPush = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
            TouchPush = OVRInput.Get(OVRInput.Button.PrimaryTouchpad);
        }

        if (TriggerPush)
        {
          //  quicktext.text = "TP Yes";
            TouchPress(TriggerPush);

        }
        else
        {

        }
        if(BackButton)
        {
            StartCoroutine(GoingBack());
        }

        TriggerPull();

    }

    public void CraneMovement(Rigidbody rbody, float speed,Transform crane)
    {
        rbody.velocity = -crane.forward * touchpoint.y * (speed * Time.deltaTime) + -crane.right * touchpoint.x * (speed * Time.deltaTime);

    }

    void TriggerPull()
    {
        if(TriggerPush)
        {
            GrabberControl.instance.LetGoFull();

        }
    }

    void TouchPress(bool push)
    {
        if (push)
        {
            GrabberControl.instance.VRCraneLocation(push);
           
        }
    }

    IEnumerator GoingBack()
    {
        SceneFader.instance.FadeChoice();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}

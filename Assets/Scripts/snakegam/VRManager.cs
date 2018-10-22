using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VRManager : MonoBehaviour {

    Vector2 touchPoint;
    public OVRInput.Controller controller;
    public static VRManager instance;
    public Text debugText;
    bool backbutton = false;
    
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
     //   controller = OVRInput.GetActiveController();
  
        bool touchClick = OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad);
        backbutton  = OVRInput.GetDown(OVRInput.Button.Back);

        if(backbutton)
        {
            StartCoroutine(GoingBack());
        }

        if (touchClick)
        {
            Snake.instance.DirectionChanged = true;
            touchPoint = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
            float TouchAngle = Vector2.SignedAngle(Vector2.up, touchPoint.normalized);
            controllerInput(TouchAngle);
       //     debugText.text = "input: " + TouchAngle;
        }
    }

    IEnumerator GoingBack()
    {
        SceneFader.instance.FadeChoice();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }

    public void controllerInput( float touchLocation)
    {
        if (touchLocation < 0)
            touchLocation += 360f;

        if(touchLocation>=315f || touchLocation<=45f)
        {
            Snake.instance.SnakeMovementDirection = Snake.snakeDirection.up;
        }else if(touchLocation>45f && touchLocation<=135f)
        {
            Snake.instance.SnakeMovementDirection = Snake.snakeDirection.left;
        }
        else if(touchLocation>135f && touchLocation<=225f)
        {
            Snake.instance.SnakeMovementDirection = Snake.snakeDirection.down;
        }
        else if(touchLocation>225f && touchLocation<315f)
        {
            Snake.instance.SnakeMovementDirection = Snake.snakeDirection.right;
        }
    }
 



}

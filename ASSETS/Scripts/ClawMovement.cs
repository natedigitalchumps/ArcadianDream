using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMovement : MonoBehaviour {

   
    public float speed = 2;
    public GrabberControl grabber;

 
    // Update is called once per frame
    void FixedUpdate () {

        float hor;
        float front;

        hor = Input.GetAxis("Horizontal");
        front = Input.GetAxis("Vertical");

       // hor = Mathf.Round(hor);
       // front = Mathf.Round(front);
 
        transform.Translate(transform.right * hor * (speed * Time.deltaTime));
        transform.Translate(transform.forward * front * (speed * Time.deltaTime));
    }
}

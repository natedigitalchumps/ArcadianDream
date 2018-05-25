using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMovement : MonoBehaviour {

    Vector2 startPos;
    float ParentSize;
    public float speed = 2;
    private void Awake()
    {
        startPos.x = transform.localPosition.x;
        startPos.y = transform.localPosition.z;

        ParentSize = transform.parent.localScale.x;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float hor;
        float front;

        hor = Input.GetAxis("Horizontal");
        front = Input.GetAxis("Vertical");

        if((transform.localPosition.x>=startPos.x) && (transform.localPosition.z>=startPos.y))
        {
            transform.Translate( -transform.right * hor * Time.deltaTime);
            print(hor);
            transform.Translate(transform.forward * front * Time.deltaTime);
            print(front);
        }
	}
}

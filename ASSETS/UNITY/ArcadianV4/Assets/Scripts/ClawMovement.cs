using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMovement : MonoBehaviour {

    Vector2 startPos;
    float ParentSize;
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
        float ver;


	}
}

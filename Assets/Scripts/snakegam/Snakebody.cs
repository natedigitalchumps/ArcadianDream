using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snakebody : MonoBehaviour {

    public Transform pointFollower;
    public Transform enderPoint;

    private void Awake()
    {
        enderPoint = transform.GetChild(0);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(pointFollower!=null)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointFollower.position, 5f);
           // transform.rotation = pointFollower.rotation;
        }

	}
}

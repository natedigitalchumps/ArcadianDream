using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {

    public GameObject head;
    public GameObject tail;
    public GameObject snakebodyobj;
    Transform TailFollowPoint;
    public enum direction { up,down,left,right};
    public direction currentdirection;
    public List<Snakebody> bodylist = new List<Snakebody>();

	// Use this for initialization
	void Start () {
        currentdirection = direction.right;
	}

    // Update is called once per frame
    void Update()
    {
        SnakeDirection();

        transform.Translate(Vector3.forward * Time.deltaTime * 5f);
        tail.transform.rotation = head.transform.rotation;
        if (TailFollowPoint != null)
            tailFollow();
        else
            tail.transform.position = Vector3.MoveTowards(tail.transform.position, head.transform.position,5f);

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            createbody();
        }
#endif


    }

    void SnakeDirection()
    {

#if UNITY_EDITOR
        switch (currentdirection)
        {
            case direction.right:
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    head.transform.Rotate(head.transform.rotation.x, head.transform.rotation.y - 90f, head.transform.rotation.z);
                    currentdirection = direction.up;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    head.transform.Rotate(head.transform.rotation.x, head.transform.rotation.y + 90f, head.transform.rotation.z);
                    currentdirection = direction.down;
                }

                break;
            case direction.up:
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    head.transform.Rotate(head.transform.rotation.x, head.transform.rotation.y - 90f, head.transform.rotation.z);
                    currentdirection = direction.left;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    head.transform.Rotate(head.transform.rotation.x, head.transform.rotation.y + 90f, head.transform.rotation.z);
                    currentdirection = direction.right;
                }

                break;

        }
#endif




    }

    void tailFollow()
    {
        tail.transform.position = Vector3.MoveTowards(tail.transform.position, TailFollowPoint.position, 5f);
        tail.transform.rotation = head.transform.rotation;
    }


    public void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("badhitobj"))
        {
            print("I hit a bad obj");
            ///end game
        }
        else
        {
            print("I ate food");
            //increase points
        }
    }

    void createbody()
    {
        GameObject obj;
        
        if (bodylist.Count != 0)
             obj = Instantiate(snakebodyobj, bodylist[bodylist.Count-1].transform.position, head.transform.rotation);
        else
             obj = Instantiate(snakebodyobj, head.transform.position, head.transform.rotation);
        Snakebody body =  obj.GetComponent<Snakebody>();
        bodylist.Add(body);
        if (bodylist.Count != 1)
            body.pointFollower = bodylist[bodylist.Count - 2].transform.GetChild(0).transform;
        else
            body.pointFollower = head.transform;
        TailFollowPoint = bodylist[bodylist.Count - 1].transform.GetChild(0).transform;
        
                 
    }
}

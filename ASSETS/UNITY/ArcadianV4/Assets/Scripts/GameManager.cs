using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// This is a core Singleton to act as a master GameManager
// It is a Singleton so be responsible.
//
public class GameManager : MonoBehaviour {

	public static GameManager instance = null; // Singleton Instance

	// Manages the Singleton Instance.
	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {

            Destroy(gameObject);
		}
	}



	// Use this for initialization
	void Start () {
 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

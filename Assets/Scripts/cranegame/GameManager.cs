using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// This is a core Singleton to act as a master GameManager
// It is a Singleton so be responsible.
//
public class GameManager : MonoBehaviour {

    public enum BuildPlatform {UnityEditor,Android};
    public BuildPlatform buildplatform;

	public static GameManager instance = null; // Singleton Instance
    public float rotatespeed = 1.5f;

	// Manages the Singleton Instance.
	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {

            Destroy(gameObject);
		}

#if UNITY_EDITOR
        buildplatform = BuildPlatform.UnityEditor;
#elif UNITY_ANDROID
    buildplatform = BuildPlatform.Android;
#endif
    }



    // Use this for initialization
    void Start () {
 
	}
	
	// Update is called once per frame
	void Update () {
        RenderSettings.skybox.SetFloat("_Rotation", rotatespeed * Time.time);
	}
}

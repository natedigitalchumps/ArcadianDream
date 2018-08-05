using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldObjectInteractor : MonoBehaviour {

    public string ObjName;
    public int label;
    public int group = 0;

	// Use this for initialization
	void Start () {
		if(gameObject.CompareTag("game"))
        {

            group = 3;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   public void ClickedOn()
    {
        

        switch (group)
        {
            case 1:
                MainManager.MainMan.gamestate = MainManager.GameState.play;
                MainManager.MainMan.NextAction();
                break;

            case 2:
                MainManager.MainMan.gamestate = MainManager.GameState.end;
                MainManager.MainMan.NextAction();
                break;

            case 3:
                MainManager.MainMan.GameSceneStarter(label);
                break;
            case 0:
                NameChooser.instance.LetterChanger(label);

                break;
            case -1:
                  MainManager.MainMan.gamestate = MainManager.GameState.start;
                  MainManager.MainMan.NextAction();
                break;


        }

  

        
        

    }

}

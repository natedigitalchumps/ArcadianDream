using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldObjectInteractor : MonoBehaviour {

    AudioSource asource;
    public AudioClip aclip;
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

    void AudioClicker()
    {
        asource.clip = aclip;
        asource.playOnAwake = false;
        asource.loop = false;
        asource.volume = .4f;
        asource.Play();
    }

   public void ClickedOn()
    {
        if(asource== null)
        {
            asource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        }

        AudioClicker();

        switch (group)
        {
            case 1:
                MainManager.MainMan.gamestate = MainManager.GameState.play;
                MainManager.MainMan.NextAction(label);
                break;

            case 2:
                MainManager.MainMan.gamestate = MainManager.GameState.end;
                MainManager.MainMan.NextAction(label);
                break;

            case 3:
                MainManager.MainMan.GameSceneStarter(label);
                break;
            case 0:
                NameChooser.instance.LetterChanger(label);

                break;
            case -1:
                NameChooser.instance.NameSet();
                  MainManager.MainMan.gamestate = MainManager.GameState.start;
                  MainManager.MainMan.NextAction(0);
                break;
            case 4:
                MainManager.MainMan.AboutToEndGame(label);
            //    print("clickedon");
                break;
        }

      //  StartCoroutine(AudioRemover());
        
    }

    IEnumerator AudioRemover()
    {
        yield return new WaitForSeconds(.4f);
        asource = null;
        Destroy(gameObject.GetComponent("AudioSource"));
    }

}

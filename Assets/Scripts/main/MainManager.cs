using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour {

    public List<GameObject> Faces1 = new List<GameObject>();
    public List<GameObject> Faces2 = new List<GameObject>();
    public List<GameObject> Names = new List<GameObject>();

    public GameObject gamegroup;
    public GameObject FaceGroup1;

    public int UseCount = -1;
    public string tempTextName;
    public int tempFace1;
    public int tempFace2;
    public OVRInput.Controller controller;
    public Transform head;
    public static MainManager MainMan;

    public enum GameState {name,start,play,end};
    public GameState gamestate;

    private void Awake()
    {

        if (PlayerPrefs.GetString("state", "play") == GameState.play.ToString())
        {
            gamestate = GameState.play;

            for (int i = 0; i < Names.Count; i++)
            {
                Names[i].SetActive(false);
            }
            NextAction(PlayerPrefs.GetInt("tempnum1"));
        }

        if (!PlayerPrefs.HasKey("usecount"))
        {
            UseCount = 0;
        }else
        {
            UseCount = PlayerPrefs.GetInt("usecount");
            tempTextName = PlayerPrefs.GetString("tempName", "");
            tempFace1 = PlayerPrefs.GetInt("tempnum1");
            
        }
        //debug
        print(UseCount);
        //debug

        MainMan = this;

        for(int i=0;i<Faces1.Count;i++)
        {
            WorldObjectInteractor inter = Faces1[i].GetComponent<WorldObjectInteractor>();
            inter.group = 1;
            inter = Faces2[i].GetComponent<WorldObjectInteractor>();
            inter.group = 4;

        }
    
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        OVRInput.Update();
        RaycastHit hit;
        if(Physics.Raycast(head.position,head.forward,out hit,Mathf.Infinity))
        {
            VRController();
            PCTesting(hit);
        }

        

    }

    void VRController()
    {

    }

  public void NextAction(int intValue)
    {
        switch(gamestate)
        {
            case GameState.play:
                tempFace1 = intValue;
                print("going to playerprefs "+ tempFace1);
                PlayerPrefs.SetInt("tempnum1",tempFace1);
                StartCoroutine(TurnOffFaces());
                break;
            case GameState.end:
                StartCoroutine(TurnOnFaces());
                break;
            case GameState.start:
                FaceGroup1.SetActive(true);
                for (int i = 0; i < Names.Count; i++)
                {
                    Names[i].SetActive(false);
                }
                break;

        }
    }

    public IEnumerator TurnOffFaces()
    {
        for(int i=0;i<Faces1.Count;i++)
        {
            Faces1[i].gameObject.SetActive(false);
            yield return new WaitForSeconds(.3f);

        }
        gamegroup.SetActive(true);
    }

    public IEnumerator TurnOnFaces()
    {

        gamegroup.SetActive(false);
        for (int i = 0; i < Faces2.Count; i++)
        {
            Faces2[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(.3f);

        }
    }

    public void  GameSceneStarter(int SceneChoice)
    {
        if (SceneChoice == 60)
        {
            // game 1
            PlayerPrefs.SetString("state", GameState.play.ToString());

        } else if(SceneChoice == 70)
        {
            PlayerPrefs.SetString("state", GameState.play.ToString());
            //game2
            SceneManager.LoadScene(1);
        }
        PlayerPrefs.SetString("tempName", tempTextName);
        PlayerPrefs.SetInt("usecount", UseCount);
    }

    


    public void PCTesting(RaycastHit rhit)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (rhit.transform.CompareTag("fakeUI") || rhit.transform.CompareTag("game"))
            {
                WorldObjectInteractor interact = rhit.transform.GetComponent<WorldObjectInteractor>();
                interact.ClickedOn();
            }
        }
    }

    public void OnApplicationQuit()
    {
        gamestate = GameState.name;
        PlayerPrefs.SetString("state", gamestate.ToString());
        UseCount++;
        PlayerPrefs.SetInt("usecount", UseCount);
    }

    public void AboutToEndGame(int ChosenFace)
    {
        tempFace2 = ChosenFace;
        print(tempFace1);
        print(tempFace2);
        PlayerPrefs.SetString("User" + UseCount, tempTextName + tempFace1.ToString() + tempFace2.ToString());
        print(PlayerPrefs.GetString("User" + UseCount));
    }

}

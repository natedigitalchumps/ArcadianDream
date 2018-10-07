﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour {

    public List<GameObject> Faces1 = new List<GameObject>();
    public List<GameObject> Faces2 = new List<GameObject>();
    public List<GameObject> Names = new List<GameObject>();
    //
    public string InfoFileSave;

    bool inputActive = true;

    public GameObject gamegroup;
    public GameObject FaceGroup1;

    public int UseCount = -1;
    public string tempTextName;
    public int tempFace1=0;
    public int tempFace2=0;
    public OVRInput.Controller controller;
    public Transform head;
    public static MainManager MainMan;
    bool TriggerPush;
    public enum GameState {name,start,play,end};
    public GameState gamestate;

    private void Awake()
    {
        InfoFileSave = "PlayData.txt";
     //   
        if (!PlayerPrefs.HasKey("usecount"))
        {
            UseCount = 0;
        }
        tempFace1 = PlayerPrefs.GetInt("tempnum1");
        
        if (tempFace1 ==0)
        {
            PlayerPrefs.SetInt("tempnum1", 0);
            gamestate = GameState.name;
        }
       // if (PlayerPrefs.GetString("state", "play") == GameState.play.ToString())
       else
        {
            SceneFader.instance.FadeChoice();
            UseCount = PlayerPrefs.GetInt("usecount");
            tempTextName = PlayerPrefs.GetString("tempName", "");
            gamestate = GameState.play;

            for (int i = 0; i < Names.Count; i++)
            {
                Names[i].SetActive(false);
            }
            NextAction(tempFace1);
        } 

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


    // Update is called once per frame
    void Update() {
        OVRInput.Update();
        RaycastHit hit;
        TriggerPush = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger);
        if (Physics.Raycast(head.position,head.forward,out hit,Mathf.Infinity))
        {
            if(inputActive)
                VRController(hit);
#if UNITY_EDITOR
            PCTesting(hit);
#endif
        }

        

    }

    void VRController(RaycastHit rhit)
    {
        

        if(TriggerPush)
        {
            if (rhit.transform.CompareTag("fakeUI") || rhit.transform.CompareTag("game"))
            {
                WorldObjectInteractor interact = rhit.transform.GetComponent<WorldObjectInteractor>();
                interact.ClickedOn();
            }

        }

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
            yield return new WaitForSeconds(.1f);

        }
        gamegroup.SetActive(true);
    }

    public IEnumerator TurnOnFaces()
    {

        gamegroup.SetActive(false);
        for (int i = 0; i < Faces2.Count; i++)
        {
            Faces2[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(.1f);

        }
    }

    public void  GameSceneStarter(int SceneChoice)
    {
        if (SceneChoice == 60)
        {
            // game 1
            //DoFade(2);
            PlayerPrefs.SetString("state", GameState.play.ToString());

        } else if(SceneChoice == 70)
        {
            PlayerPrefs.SetString("state", GameState.play.ToString());
            //game2
            StartCoroutine(DoFade(1));
        }
        PlayerPrefs.SetString("tempName", tempTextName);
        PlayerPrefs.SetInt("usecount", UseCount);
    }

    IEnumerator DoFade(int sceneNum)
    {
        SceneFader.instance.FadeChoice();
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(sceneNum);
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

 

    public void AboutToEndGame(int ChosenFace)
    {
        tempFace2 = ChosenFace;
       print(tempFace1);
       print(tempFace2);
        string UserInfo;
        PlayerPrefs.SetString("User" + UseCount, tempTextName + tempFace1.ToString() + tempFace2.ToString());
         print(PlayerPrefs.GetString("User" + UseCount));
        UserInfo = UseCount + "\n"+ tempTextName + "\n" + tempFace1.ToString() + "\n" + tempFace2.ToString() + "\n\n" ;
        gamestate = GameState.name;
        PlayerPrefs.SetString("state", gamestate.ToString());
        PlayerPrefs.SetInt("tempnum1", 0);
        PlayerPrefs.SetInt("tempnum2", 0);
        UseCount++;
        PlayerPrefs.SetInt("usecount", UseCount);
        PlayerPrefs.Save();
       
        DataWriter(UserInfo);
        inputActive = false;
        print("info saved");
    }
    void DataWriter(string currentUser)
    {
        File.AppendAllText(Path.Combine(UnityEngine.Application.persistentDataPath, InfoFileSave), currentUser);
        
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("tempnum1", 0);
        PlayerPrefs.SetInt("tempnum2", 0);
        gamestate = GameState.name;
        PlayerPrefs.SetString("state", gamestate.ToString());
    }
}

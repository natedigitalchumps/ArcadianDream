using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeGameManager : MonoBehaviour {

    public static SnakeGameManager manager;


    void Awake()
    {
        manager = this;
    }


   public  void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }
}

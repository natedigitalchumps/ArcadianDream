using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour {

    public static GamePlayManager instance;
    public int score=0;
    public int timer = 100;
    public Text TimerText;
    public Text ScoreText;
    int timer59 = 59;

    public void GameTimer()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {

        yield return new WaitForEndOfFrame();

    }

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

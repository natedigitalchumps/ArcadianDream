using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
        
        while(timer>=0)

        {
            if(timer>=110)
            {
                TimerText.text = "1:" + (timer - 100);
                timer--;
            }else if(timer<=109 && timer>=101)
            {
                TimerText.text = "1:0" + (timer - 100);
                timer--;
            }
            else if(timer==100)
            {
                TimerText.text = "1:00";
                timer = 59;
            }
            else if(timer<10)
            {
                TimerText.text = ":0" + timer;
                timer--;
            }

            else
            {
                TimerText.text = ":" + timer;
                timer--;
                
            }
            

            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(2f);
        GotoMainScene();
    }

    private void Awake()
    {
        TimerText.text = "1:30";
        instance = this;
        GameTimer();
    }


	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetMouseButtonDown(1))
        {
            GotoMainScene(); 
        }

	}

    public void IncreaseScore()
    {
        score++;
      ScoreText.text = score.ToString();
    }

    public void GotoMainScene()
    {
        StartCoroutine(GoingBack());
    }


    IEnumerator GoingBack()
    {
        SceneFader.instance.FadeChoice();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {

    public static UIControl instant;
    public Text ScoreText;
    public int ScoreValue=0;
    public int GameTimeValue = 0;
    public Text ClockCounter;
    public int worldCounter = 10000;
    public GameObject overObj;
    void Awake()
    {

        if(SceneFader.instance!=null)
            SceneFader.instance.FadeChoice();
        StartCoroutine(GameTimer());
        instant = this;
    }

    public void ScoreInc()
    {
        ScoreValue++;
        ScoreText.text = "Score: " + ScoreValue.ToString();
    }

    IEnumerator GameTimer()
    {
        int count = 0;
        while(count<worldCounter)
        {
            yield return new WaitForSeconds(1f);
            count++;
            GameTimeValue = count;
            ClockCounter.text = "Time: " + GameTimeValue.ToString();
        }
    }

    void Update()
    { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour {

    Image faderimage;
 public   enum FadeState { fadein,fadeout};
    public FadeState currentFadeState;
    Color currentColor;
    static public SceneFader instance = null;
    public static bool created = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
        }
           
        //If instance already exists and it's not this:
        else if (SceneFader.instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        if (faderimage ==null)
        faderimage = transform.GetChild(0).GetComponent<Image>();
        currentColor = faderimage.color;
       

    }

    public void Start()
    {
        FadeChoice();
        print("fading the scene");
    }


    public void FadeChoice()
    {
        Canvas canv = GetComponent<Canvas>();
        if(canv.worldCamera == null)
        {
            canv.worldCamera = Camera.main;
        }
        switch(currentFadeState)
        {
            case FadeState.fadein:

                StartCoroutine(FadeIn());
                break;

            case FadeState.fadeout:

                StartCoroutine(FadeOut());
                break;
        }

    }

    IEnumerator FadeIn()

    {
        while(currentColor.a>.01f)
        {
            yield return new WaitForSeconds(.1f);
            currentColor.a -= .2f;
            faderimage.color = currentColor;
        }
        currentFadeState = FadeState.fadeout;
    }

    IEnumerator FadeOut()
    {
        {
            while (currentColor.a < .99f)
            {
                yield return new WaitForSeconds(.1f);
                currentColor.a += .2f;
                faderimage.color = currentColor;
            }
            currentFadeState = FadeState.fadein;
        }
    }
}

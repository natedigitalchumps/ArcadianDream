using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameChooser : MonoBehaviour {
    public List<string> FirstName = new List<string>();
    public List<string> LastName = new List<string>();
   public  int firstCounter = 0;
   public  int lastCounter = 0;
    public Text first;
    public Text last;


    public static NameChooser instance;

        void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpLetter(int side)
    {
        if (side == 0)
        {
            firstCounter++;
            if(firstCounter>FirstName.Count-1)
            {
                firstCounter = 0;
            }
            first.text = FirstName[firstCounter];
        }
        else
        {
            lastCounter++;
            if (lastCounter > FirstName.Count-1)
            {
                lastCounter = 0;
            }
            last.text = LastName[lastCounter];
        }
    }

    public void DownLetter(int side)
    {
        if(side == 0)
        {
            firstCounter--;
            if (firstCounter < 0)
            {
                firstCounter = FirstName.Count-1;
            }
            first.text = FirstName[firstCounter];
        }
        else
        {
            lastCounter--;
            if (lastCounter < 0)
            {
                lastCounter = LastName.Count-1;
            }
            last.text = LastName[lastCounter];
        }
    }

    public void LetterChanger(int num)
    {
        switch(num)
        {
            case 100:
                UpLetter(0);
                break;
            case 101:
                DownLetter(0);
                break;
            case 102:
                UpLetter(1);
                break;
            case 103:
                DownLetter(1);
                break;
        }
    }

    public void NameSet()
    {
        MainManager.MainMan.tempTextName = first.text+last.text;
    }

}

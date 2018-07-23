using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour {

    public string StartFeel;
    public string EndFeel;

    public SaveState state;
    public static DataSaver instance { set; get; }


}

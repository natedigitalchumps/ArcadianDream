using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [ExecuteInEditMode]
    public class tdisplay : MonoBehaviour
    {
        public int displayID;

        void Awake()
        {
            GetComponent<Camera>().targetDisplay = displayID;
        }
    }


using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScenesManager : EditorWindow
{
    [MenuItem("Tools/Scenes Manager")]
    static void ShowWindow()
    {
        GetWindow(typeof(ScenesManager), true, "shmup");
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

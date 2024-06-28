using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static GlobalVariables instance;

    public bool isContinue;
    public bool saveLevel;
    // Start is called before the first frame update
    private void Awake()
    {
        MakeSingleton();
       // instance = this;
    }
    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}

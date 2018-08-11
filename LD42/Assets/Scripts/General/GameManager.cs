// Date   : 11.08.2018 15:52
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager main;

    private void Awake()
    {
        main = this;
    }

    public void KillPlayer()
    {
        Debug.Log("Oh no you were killed!");
    }

    void Start () {
    
    }

    void Update () {
    
    }
}

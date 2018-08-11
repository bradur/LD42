// Date   : 11.08.2018 23:23
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public static UIManager main;

    [SerializeField]
    private Popup popup;

    private void Awake()
    {
        main = this;
    }

    public void ShowPopup(string title, string message)
    {
        Time.timeScale = 0f;
        popup.Show(title, message);
    }

    public void HidePopup()
    {
        Time.timeScale = 1f;
        popup.Hide();
    }

    void Start () {
    
    }

    void Update () {
    
    }
}

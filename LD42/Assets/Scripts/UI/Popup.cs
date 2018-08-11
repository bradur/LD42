// Date   : 11.08.2018 23:33
// Project: LD42
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Popup : MonoBehaviour {

    [SerializeField]
    private Text txtTitle;
    [SerializeField]
    private Text txtMessage;

    [SerializeField]
    private Animator animator;

    public void Show(string title, string message)
    {
        txtTitle.text = title;
        txtMessage.text = message;
        animator.SetTrigger("Show");
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
    }
}

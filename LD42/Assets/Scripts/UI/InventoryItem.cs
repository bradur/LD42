// Date   : 12.08.2018 07:48
// Project: LD42
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryItem : MonoBehaviour {

    [SerializeField]
    private Color highlightColor;

    [SerializeField]
    private Image iconBackground;

    [SerializeField]
    private Image keyBackground;

    [SerializeField]
    private Text txtCount;

    private int max = 0;
    private int current = 0;

    private Color originalIconBackground;
    private Color originalKeyBackground;

    private void Start()
    {
        originalIconBackground = iconBackground.color;
        originalKeyBackground = keyBackground.color;
    }

    public void SetCount(int max)
    {
        this.max = max;
        current = max;
        ShowCount();
    }


    private void ShowCount()
    {
        txtCount.text = string.Format("{0} / {1}", current, max);
    }

    public void Select()
    {
        keyBackground.color = highlightColor;
        iconBackground.color = highlightColor;
    }

    public void Deselect()
    {
        keyBackground.color = originalKeyBackground;
        iconBackground.color = originalIconBackground;
    }

    public void Use()
    {
        current -= 1;
        ShowCount();
    }
}

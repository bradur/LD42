// Date   : 11.08.2018 23:23
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{

    public static UIManager main;

    [SerializeField]
    private Popup popup;

    [SerializeField]
    private Inventory inventory;

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

    public void UseTool(ToolType toolType)
    {
        inventory.UseTool(toolType);
        if (toolType == ToolType.Block)
        {
            SoundManager.main.PlaySound(SoundType.PlaceBlock);
        }
        else if (toolType == ToolType.Dynamite)
        {
            SoundManager.main.PlaySound(SoundType.PlaceDynamite);
        }
        else if (toolType == ToolType.Bomb)
        {
            SoundManager.main.PlaySound(SoundType.PlaceBomb);
        }
    }

    public void SelectTool(ToolType toolType)
    {
        inventory.SelectTool(toolType);
    }

    public void SetToolCounts(int bombCount, int dynamiteCount, int blockCount)
    {
        inventory.SetToolCounts(bombCount, dynamiteCount, blockCount);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}

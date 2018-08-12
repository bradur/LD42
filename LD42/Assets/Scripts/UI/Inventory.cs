// Date   : 12.08.2018 07:43
// Project: LD42
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inventory : MonoBehaviour
{

    [SerializeField]
    private InventoryItem bombItem;
    [SerializeField]
    private InventoryItem dynamiteItem;
    [SerializeField]
    private InventoryItem blockItem;

    private PlayerUseTool playerUseTool;

    private void Start()
    {

    }

    public void SelectTool(ToolType toolType)
    {
        if (toolType == ToolType.Bomb)
        {
            bombItem.Select();
        }
        else if (toolType == ToolType.Dynamite)
        {
            dynamiteItem.Select();
        }
        else if (toolType == ToolType.Block)
        {
            blockItem.Select();
        }
    }

    public void SetToolCounts(int bombCount, int dynamiteCount, int blockCount)
    {
        bombItem.SetCount(bombCount);
        dynamiteItem.SetCount(dynamiteCount);
        blockItem.SetCount(blockCount);
    }

    private void DeselectAllTools()
    {
        bombItem.Deselect();
        dynamiteItem.Deselect();
        blockItem.Deselect();
    }

    private void GetPlayerUseTool()
    {
        if (playerUseTool == null)
        {
            playerUseTool = GameManager.main.GetPlayerUseTool();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetPlayerUseTool();
            playerUseTool.SwitchTool(ToolType.Bomb);
            DeselectAllTools();
            bombItem.Select();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetPlayerUseTool();
            playerUseTool.SwitchTool(ToolType.Dynamite);
            DeselectAllTools();
            dynamiteItem.Select();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GetPlayerUseTool();
            playerUseTool.SwitchTool(ToolType.Block);
            DeselectAllTools();
            blockItem.Select();
        }
    }

    public void UseTool(ToolType toolType)
    {
        if (toolType == ToolType.Bomb)
        {
            bombItem.Use();
        }
        else if (toolType == ToolType.Dynamite)
        {
            dynamiteItem.Use();
        }
        else if (toolType == ToolType.Block)
        {
            blockItem.Use();
        }
    }

}

// Date   : 11.08.2018 11:50
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;

public enum ToolType
{
    None,
    Dynamite,
    Bomb,
    Block
}

public class PlayerUseTool : MonoBehaviour
{

    [SerializeField]
    private Dynamite dynamitePrefab;

    [SerializeField]
    private Dynamite bombPrefab;

    [SerializeField]
    private Wall wallPrefab;

    [SerializeField]
    private ToolType currentTool;

    private float toolUseTimer = 0f;

    MapGrid mapGrid;

    int bombCount;
    int dynamiteCount;
    int blockCount;

    public void Initialize(MapGrid mapGrid)
    {
        this.mapGrid = mapGrid;
    }

    public void SwitchTool(ToolType toolType)
    {
        currentTool = toolType;
    }

    void Start()
    {
        UIManager.main.SelectTool(currentTool);
    }

    public void SetToolCounts(int bombCount, int dynamiteCount, int blockCount)
    {
        this.bombCount = bombCount;
        this.dynamiteCount = dynamiteCount;
        this.blockCount = blockCount;
    }

    void Update()
    {
        toolUseTimer -= Time.deltaTime;
        if (toolUseTimer <= 0f && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X)))
        {
            if (currentTool == ToolType.Dynamite && dynamiteCount > 0)
            {
                dynamiteCount -= 1;
                Dynamite dynamite = Instantiate(dynamitePrefab);
                dynamite.transform.SetParent(transform.parent, false);
                dynamite.Initialize(
                    (int)(transform.localPosition.x + 0.5f),
                    (int)(transform.localPosition.y + 0.5f),
                    mapGrid
                );
                toolUseTimer = dynamite.UseInterval;
                UIManager.main.UseTool(currentTool);
            }
            else if (currentTool == ToolType.Bomb && bombCount > 0)
            {
                bombCount -= 1;
                Dynamite bomb = Instantiate(bombPrefab);
                bomb.transform.SetParent(transform.parent, false);
                bomb.Initialize(
                    (int)(transform.localPosition.x + 0.5f),
                    (int)(transform.localPosition.y + 0.5f),
                    mapGrid
                );
                toolUseTimer = bomb.UseInterval;
                UIManager.main.UseTool(currentTool);
            }
            else if (currentTool == ToolType.Block && blockCount > 0)
            {
                blockCount -= 1;
                Wall wall = Instantiate(wallPrefab);
                wall.transform.SetParent(transform.parent, false);
                wall.Initialize(
                    (int)(transform.localPosition.x + 0.5f),
                    (int)(transform.localPosition.y + 0.5f)
                );
                toolUseTimer = wall.UseInterval;
                UIManager.main.UseTool(currentTool);
            }
        }
    }
}

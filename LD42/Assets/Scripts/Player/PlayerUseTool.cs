// Date   : 11.08.2018 11:50
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;

public enum ToolType
{
    None,
    Dynamite
}

public class PlayerUseTool : MonoBehaviour {

    [SerializeField]
    private Dynamite dynamitePrefab;

    [SerializeField]
    private MapGrid mapGrid;

    [SerializeField]
    private ToolType currentTool;

    private float toolUseTimer = 0f;

    void Start () {
    
    }

    void Update () {
        toolUseTimer -= Time.deltaTime;
        if (toolUseTimer <= 0f && Input.GetKeyDown(KeyCode.Space))
        {
            if (currentTool == ToolType.Dynamite)
            {
                Dynamite dynamite = Instantiate(dynamitePrefab);
                dynamite.transform.SetParent(transform.parent);
                dynamite.Initialize(
                    (int)(transform.localPosition.x + 0.5f),
                    (int)(transform.localPosition.y + 0.5f),
                    mapGrid
                );
                toolUseTimer = dynamite.UseInterval;
            }
        }
    }
}

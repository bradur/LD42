// Date   : 22.04.2017 08:44
// Project: Out of This Small World
// Author : bradur

using UnityEngine;
using System.Collections.Generic;
using TiledSharp;
using System.Xml.Linq;
using UnityEngine.SceneManagement;

enum LayerType
{
    None,
    Ground,
    Slime,
    Wall,
    Player
}


public class LevelLoader : MonoBehaviour
{
    string[] layerTypes =
    {
        "None",
        "Ground",
        "Slime",
        "Wall",
        "Player"
    };

    [SerializeField]
    private TiledMesh tiledMeshPrefab;

    [SerializeField]
    private Material groundMaterial;

    [SerializeField]
    private TextAsset debugMap;

    [SerializeField]
    private Transform world;

    [SerializeField]
    private MapGrid mapGrid;

    [SerializeField]
    private Transform otherStuff;

    [SerializeField]
    private PlayerMovement playerPrefab;

    [SerializeField]
    private List<TextAsset> levels;

    [SerializeField]
    private int nextLevel = 0;
    private int currentLevel = 0;

    void Start()
    {
        //Init(debugMap);
        OpenNextLevel();
    }

    void Update()
    {
    }

    public void OpenNextLevel()
    {
        if (nextLevel < levels.Count)
        {
            Init(levels[nextLevel]);
            currentLevel = nextLevel;
        }
        else
        {
            GameManager.main.TheEnd();
        }
        nextLevel += 1;
    }

    public void RestartLevel()
    {
        Init(levels[currentLevel]);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Init(TextAsset mapFile)
    {
        XDocument mapX = XDocument.Parse(mapFile.text);
        TmxMap map = new TmxMap(mapX);

        PlayerMovement player = Instantiate(playerPrefab);
        GameManager.main.SetPlayer(player);
        GameManager.main.SetToolCounts(
            Tools.IntParseFast(map.Properties["Bombs"]),
            Tools.IntParseFast(map.Properties["Dynamites"]),
            Tools.IntParseFast(map.Properties["Blocks"])
        );

        for (int index = 0; index < map.ObjectGroups.Count; index += 1)
        {
            for (int oIndex = 0; oIndex < map.ObjectGroups[index].Objects.Count; oIndex += 1)
            {
                TmxObject tmxObject = map.ObjectGroups[index].Objects[oIndex];
                int layerTypeIndex = System.Array.IndexOf(layerTypes, map.ObjectGroups[index].Properties["Type"]);
                LayerType layerType = (LayerType)layerTypeIndex;
                if (layerType == LayerType.Player)
                {
                    player.transform.SetParent(world, false);
                    player.transform.localPosition = new Vector3((float)tmxObject.X / 64f, map.Height - (float)tmxObject.Y / 64f, 0);
                    player.GetComponent<PlayerUseTool>().Initialize(mapGrid);
                }
                //SpawnObject(tmxObject, world.GetItemContainer().transform, map.Width, map.Height);
            }
        }
        mapGrid.Initialize(map.Width, map.Height, player);
        for (int index = 0; index < map.Layers.Count; index += 1)
        {
            TmxLayer layer = map.Layers[index];
            //LayerType layerType = (LayerType)Tools.IntParseFast(layer.Properties["Type"]);
            int layerTypeIndex = System.Array.IndexOf(layerTypes, layer.Properties["Type"]);
            LayerType layerType = (LayerType)layerTypeIndex;
            TiledMesh tiledMesh;

            if (layerType == LayerType.Ground)
            {
                tiledMesh = Instantiate(tiledMeshPrefab);
                tiledMesh.transform.SetParent(otherStuff, false);
                tiledMesh.Init(map.Width, map.Height, layer, groundMaterial, transform);
                tiledMesh.GetComponent<MeshCollider>().enabled = false;
            }
            else if (layerType == LayerType.Slime)
            {
                int tilesX = map.Width;
                int tilesY = map.Height;
                for (int y = 0; y < tilesY; y++)
                {
                    for (int x = 0; x < tilesX; x++)
                    {
                        TmxLayerTile tile = layer.Tiles[(tilesY - y - 1) * tilesX + x];
                        int tileId = tile.Gid - 1;
                        if (tileId == -1)
                        {
                            continue;
                        }
                        mapGrid.AttemptToCreateSlime(-1, -1, x, y);
                    }
                }
            }
            else if (layerType == LayerType.Wall)
            {
                int tilesX = map.Width;
                int tilesY = map.Height;
                for (int y = 0; y < tilesY; y++)
                {
                    for (int x = 0; x < tilesX; x++)
                    {
                        TmxLayerTile tile = layer.Tiles[(tilesY - y - 1) * tilesX + x];
                        int tileId = tile.Gid - 1;
                        if (tileId == -1)
                        {
                            continue;
                        }
                        mapGrid.PlaceWall(x, y);
                    }
                }
            }
        }
        mapGrid.Activate();

    }

}

[System.Serializable]
public class Level : System.Object
{
    public TextAsset file;
}

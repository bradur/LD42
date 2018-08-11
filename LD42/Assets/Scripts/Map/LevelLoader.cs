// Date   : 22.04.2017 08:44
// Project: Out of This Small World
// Author : bradur

using UnityEngine;
using System.Collections.Generic;
using TiledSharp;
using System.Xml.Linq;


enum LayerType
{
    None,
    Ground,
    Slime,
    Wall
}


public class LevelLoader : MonoBehaviour
{
    string[] layerTypes =
    {
        "None",
        "Ground",
        "Slime",
        "Wall"
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

    void Start()
    {
        Init(debugMap);
    }

    void Update()
    {
    }

    private void Init(TextAsset mapFile)
    {
        XDocument mapX = XDocument.Parse(mapFile.text);
        TmxMap map = new TmxMap(mapX);
        mapGrid.Initialize(map.Width, map.Height);
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
                tiledMesh.transform.SetParent(world.transform, false);
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
                        mapGrid.AttemptToCreateSlime(x, y);
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
        for (int index = 0; index < map.ObjectGroups.Count; index += 1)
        {
            for (int oIndex = 0; oIndex < map.ObjectGroups[index].Objects.Count; oIndex += 1)
            {
                //TmxObjectGroup.TmxObject tmxObject = map.ObjectGroups[index].Objects[oIndex];
                //SpawnObject(tmxObject, world.GetItemContainer().transform, map.Width, map.Height);
            }
        }
    }

}

[System.Serializable]
public class Level : System.Object
{
    public TextAsset file;
}

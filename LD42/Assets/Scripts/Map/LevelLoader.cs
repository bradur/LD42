// Date   : 22.04.2017 08:44
// Project: Out of This Small World
// Author : bradur

using UnityEngine;
using System.Collections.Generic;
using TiledSharp;
using System.Xml.Linq;

/*
enum LayerType
{
    None,
    Ground,
    Water,
    Wall,
    Tree
}*/



public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private TiledMesh tiledMeshPrefab;

    [SerializeField]
    private Material groundMaterial;

    [SerializeField]
    private TextAsset debugMap;

    [SerializeField]
    private Transform world;

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
        for (int index = 0; index < map.Layers.Count; index += 1)
        {
            TmxLayer layer = map.Layers[index];
            //LayerType layerType = (LayerType)Tools.IntParseFast(layer.Properties["Type"]);
            TiledMesh tiledMesh;

            tiledMesh = Instantiate(tiledMeshPrefab);
            tiledMesh.transform.SetParent(world.transform, false);
            tiledMesh.Init(map.Width, map.Height, layer, groundMaterial, transform);
            //tiledMesh.transform.position = Vector3.zero;
            tiledMesh.GetComponent<MeshCollider>().enabled = false;

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

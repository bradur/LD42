// Date   : 11.08.2018 08:13
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;

public class MapGrid : MonoBehaviour {

    [SerializeField]
    private Slime slimePrefab;
    [SerializeField]
    private float slimePropagationInterval = 1f;

    Contents[,] grid;

    public void Initialize(int width, int height)
    {
        grid = new Contents[width,height];
        for (int i = 0; i < grid.GetLength(0); i += 1)
        {
            for (int j = 0; j < grid.GetLength(1); j += 1) {
                grid[i, j] = new Contents();
            }
        }

    }

    public void RemoveSlime(int x, int y)
    {
        if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1))
        {
            //Debug.Log("Can't remove slime that is out of bounds! [" + x + ", " + y + "]");
            return;
        }
        grid[x, y].slime = null;
    }

    public bool AttemptToCreateSlime(int x, int y)
    {
        Contents contents = GetContents(x, y);
        if (contents != null && contents.slime == null)
        {
            Slime slime = Instantiate(slimePrefab);
            slime.transform.SetParent(transform, false);
            slime.Initialize(x, y, slimePropagationInterval, this);
            contents.slime = slime;
            return true;
        }
        return false;
    }

    public void SetSlime(int x, int y, Slime newSlime)
    {
        if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1))
        {
            //Debug.Log("Can't set slime that is out of bounds! [" + x + ", " + y + "]");
            return;
        }
        grid[x, y].slime = newSlime;
    }

    public Contents GetContents(int x, int y)
    {
        if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1))
        {
            //Debug.Log("Can't get grid contents from out of bounds! [" + x + ", " + y + "]");
            return null;
        }
        return grid[x, y];
    }

    public bool HasSlime(int x, int y)
    {
        return GetContents(x, y).slime != null;
    }
}

[System.Serializable]
public class Contents : System.Object
{
    public Slime slime;
}

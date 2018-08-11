// Date   : 11.08.2018 08:13
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGrid : MonoBehaviour {

    [SerializeField]
    private Slime slimePrefab;
    [SerializeField]
    [Range(0.1f, 5f)]
    private float slimePropagationInterval = 1f;
    [SerializeField]
    private Wall wallPrefab;

    [SerializeField]
    private Transform wallContainer;
    [SerializeField]
    private Transform slimeContainer;

    [SerializeField]
    private Transform explosionContainer;

    [SerializeField]
    private Explosion explosionPrefab;

    Contents[,] grid;

    private int slimesCreated = 0;

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

    public void CreateExplosion(int x, int y, float explosionRadius)
    {
        Explosion explosion = Instantiate(explosionPrefab);
        explosion.transform.SetParent(explosionContainer, false);
        explosion.Initialize(x, y, explosionRadius, this);
        //DrawFilledExplosionCircle(x, y, explosionRadius);
    }

    public void ProcessExplosionTarget(Transform target)
    {
        if (target.GetComponent<PlayerMovement>())
        {
            GameManager.main.KillPlayer();
        }
        Contents contents = GetContents((int)target.localPosition.x, (int)target.localPosition.y);
        if (contents.slime)
        {
            contents.slime.Kill();
        }
        if (contents.wall)
        {
            // ?
        }
    }

    public void ProcessExplosionTargets(List<Transform> targets)
    {
        foreach (Transform target in targets)
        {
            ProcessExplosionTarget(target);
        }
    }

    /*private void DrawFilledExplosionCircle (int startX, int startY, int radius)
    {
        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                if (x * x + y * y < radius * radius + radius)
                {
                    ExplodeTile(startX + x, startY + y);
                }
            }

        }
    }

    private void ExplodeTile(int x, int y)
    {
        Contents contents = GetContents(x, y);
        if (contents != null && contents.explosion == null && contents.wall == null)
        {
            if (contents.slime)
            {
                contents.slime.Kill();
            }
            Explosion explosion = Instantiate(explosionPrefab);
            explosion.transform.SetParent(explosionContainer, false);
            explosion.Initialize(x, y);
            contents.explosion = explosion;
        }
    }*/

    public bool PlaceWall(int x, int y)
    {
        Contents contents = GetContents(x, y);
        if (contents != null && contents.wall == null)
        {
            Wall wall = Instantiate(wallPrefab);
            wall.transform.SetParent(wallContainer, false);
            wall.Initialize(x, y);
            contents.wall = wall;
            return true;
        }
        return false;
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
        if (contents != null && contents.slime == null && contents.wall == null)
        {
            slimesCreated += 1;
            Slime slime = Instantiate(slimePrefab);
            slime.name = "Slime #" + slimesCreated;
            slime.transform.SetParent(slimeContainer, false);
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
    public Wall wall;
    public Explosion explosion;
}

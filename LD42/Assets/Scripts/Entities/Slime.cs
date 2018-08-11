// Date   : 11.08.2018 08:10
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour {

    private int xPos;
    private int yPos;
    private float timer = 0f;
    private float propagationInterval;
    private bool propagating = false;
    private MapGrid mapGrid;
    private Slime slimePrefab;

    private void Start()
    {
        GetComponent<SpriteRenderer>().receiveShadows = true;
    }

    public void Initialize(int x, int y, float propagationInterval, MapGrid mapGrid)
    {
        transform.localPosition = new Vector3(x, y, 0);
        this.propagationInterval = propagationInterval;
        xPos = x;
        yPos = y;
        this.mapGrid = mapGrid;
        propagating = true;
    }

    public void Kill ()
    {
        mapGrid.RemoveSlime(xPos, yPos);
        Destroy(gameObject);
    }

    public void Propagate()
    {
        if (mapGrid.AttemptToCreateSlime(xPos, yPos + 1))
        {
            return;
        }
        if (mapGrid.AttemptToCreateSlime(xPos + 1, yPos))
        {
            return;
        }
        if (mapGrid.AttemptToCreateSlime(xPos, yPos - 1))
        {
            return;
        }
        if (mapGrid.AttemptToCreateSlime(xPos - 1, yPos))
        {
            return;
        }
        propagating = false;
    }

    private void Update()
    {
        if (propagating)
        {
            timer += Time.deltaTime;
            if (timer > propagationInterval)
            {
                Propagate();
                timer = 0f;
            }
        }
    }
}

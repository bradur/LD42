// Date   : 11.08.2018 09:44
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;

public class Dynamite : MonoBehaviour {

    [SerializeField]
    private float explosionTimer = 5f;
    private bool countDownStarted = false;
    [SerializeField]
    private float explosionRadius = 5f;

    [SerializeField]
    private float useInterval = 3f;
    public float UseInterval { get { return useInterval; } }

    private MapGrid mapGrid;

    private int xPos;
    private int yPos;

    void Start () {
    
    }

    public void Initialize(int x, int y, MapGrid mapGrid)
    {
        xPos = x;
        yPos = y;
        transform.localPosition = new Vector3(x, y, 0);
        countDownStarted = true;
        this.mapGrid = mapGrid;
    }

    private void Explode()
    {
        mapGrid.CreateExplosion(xPos, yPos, explosionRadius);
        Destroy(gameObject);
    }

    void Update () {
        if (countDownStarted)
        {
            explosionTimer -= Time.deltaTime;
            if (explosionTimer <= 0f)
            {
                Explode();
            } 
        }
    }
}

// Date   : 11.08.2018 09:06
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{

    [SerializeField]
    private float useInterval = 1f;
    public float UseInterval { get { return useInterval; } }

    [SerializeField]
    private float activateColliderDelay = 0.2f;

    private BoxCollider boxCollider;

    private bool waitForColliderActivation = false;

    public void Initialize(int x, int y)
    {
        waitForColliderActivation = true;
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        transform.localPosition = new Vector3(x, y, 0);
    }

    void Start()
    {

    }

    void Update()
    {
        if (waitForColliderActivation)
        {
            activateColliderDelay -= Time.deltaTime;
            if (activateColliderDelay <= 0f)
            {
                boxCollider.enabled = true;
                waitForColliderActivation = false;
            }
        }
    }
}

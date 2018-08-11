// Date   : 11.08.2018 09:06
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{

    public void Initialize(int x, int y)
    {
        transform.localPosition = new Vector3(x, y, 0);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}

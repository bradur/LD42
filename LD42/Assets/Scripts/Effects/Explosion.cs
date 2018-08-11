// Date   : 11.08.2018 11:41
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    [SerializeField]
    private float lifeTime = 0.2f;
    private bool alive = false;

    public void Initialize(int x, int y)
    {
        transform.localPosition = new Vector3(x, y, 0);
        alive = true;
    }

    void Start () {
    
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    void Update () {
        if (alive)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0f)
            {
                Kill();
            }
        }
    }
}

// Date   : 11.08.2018 19:12
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;

public class ExplosionCollisionHandler : MonoBehaviour {


    private Explosion explosionParent;

    public void Initialize(Explosion explosion)
    {
        explosionParent = explosion;
    }

    private void OnCollisionEnter(Collision collision)
    {
        explosionParent.HandleCollision(collision);
    }
}

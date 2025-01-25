using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FracturedObject : MonoBehaviour
{
    private float explosionForce;
    private float explosionRadius;
    private Vector2 destroyTime = new Vector2(2, 8);
    private Rigidbody[] bodies;
    private void Awake()
    {
        bodies = GetComponentsInChildren<Rigidbody>();
    }
    public void SetValues(float _explosionForce, float _explosionRadius)
    {
        explosionForce = _explosionForce;
        explosionRadius = _explosionRadius;
        Explode();
    }
    private void Explode()
    {
        Vector3 explosionPos = transform.position;
        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].AddExplosionForce(explosionForce, explosionPos, explosionRadius);
            Destroy(bodies[i].gameObject, Random.Range(destroyTime[0], destroyTime[1]));
        }
        Destroy(this.gameObject, destroyTime[1]);
    }
}

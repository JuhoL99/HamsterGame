using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FracturedObject : MonoBehaviour
{
    private float explosionForce;
    private float explosionRadius;
    private Vector3 explosionPosition;
    private Vector2 destroyTime = new Vector2(2, 8);
    private Rigidbody[] bodies;
    AudioSource audio;
    private void Awake()
    {
        bodies = GetComponentsInChildren<Rigidbody>();
        audio = gameObject.GetComponent<AudioSource>();
        audio.pitch = Random.Range(0.7f, 1.3f);
    }
    public void SetValues(float _explosionForce, float _explosionRadius, Vector3 _explosionPosition)
    {
        explosionPosition = _explosionPosition;
        explosionForce = _explosionForce;
        explosionRadius = _explosionRadius;
        Explode();
    }
    private void Explode()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
            Destroy(bodies[i].gameObject, Random.Range(destroyTime[0], destroyTime[1]));
        }
        Destroy(this.gameObject, destroyTime[1]);
    }
}

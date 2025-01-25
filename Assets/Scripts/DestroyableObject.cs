using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField] private GameObject fractured;
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionRadius;
    private Vector3 previousVelocity;
    private Rigidbody rb;
    private bool destroyed = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        previousVelocity = rb.velocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (previousVelocity.magnitude < 5f) return;
        if(!destroyed) ExplodeObject();
    }
    private void ExplodeObject()
    {
        destroyed = true;
        GameObject go = Instantiate(fractured, transform.position, transform.rotation);
        FracturedObject script = go.GetComponent<FracturedObject>();
        script.SetValues(explosionForce, explosionRadius);
        Destroy(this.gameObject);
    }
}

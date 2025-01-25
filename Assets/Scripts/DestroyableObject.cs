using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField] private GameObject fractured;
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float breakVelocity = 10f;
    private GameManager gameManager;
    private Vector3 previousVelocity;
    private Rigidbody rb;
    private bool destroyed = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameManager.instance;
    }
    private void FixedUpdate()
    {
        previousVelocity = rb.velocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        CheckPlayerCollision(collision);
        if (previousVelocity.magnitude < 5f) return;
        if(!destroyed) ExplodeObject(collision);
    }
    private void ExplodeObject(Collision collision, bool playerCaused = false)
    {
        Debug.Log(playerCaused);
        destroyed = true;
        gameManager.ObjectDestroyed(this.transform);
        GameObject go = Instantiate(fractured, transform.position, transform.rotation);
        FracturedObject script = go.GetComponent<FracturedObject>();
        Vector3 explosionPosition = playerCaused ? collision.GetContact(0).otherCollider.transform.position : transform.position;
        script.SetValues(explosionForce, explosionRadius, explosionPosition);
        Destroy(this.gameObject);
    }
    private void CheckPlayerCollision(Collision collision)
    {
        if (!(collision.collider.gameObject.tag == "Player")) return;
        if (collision.relativeVelocity.magnitude < breakVelocity) return;
        if(!destroyed) ExplodeObject(collision, true);
    }
}

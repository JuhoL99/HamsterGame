using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyDebug : MonoBehaviour
{
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Debug.Log(rb.velocity.magnitude);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterVisuals : MonoBehaviour
{
    public float angleMax, speedReachMaxAngle;
    public Transform hamsterPivot;
    Transform hamster;
    float angle;
    Rigidbody ballRB;
    public bool spinning;
    Vector3 horiVelocityDir;
    void Start()
    {
        hamster = hamsterPivot.GetChild(0);
        ballRB = GetComponent<Rigidbody>();
        horiVelocityDir = Vector3.forward;
    }

    void Update()
    {
        float angleSpeed = ballRB.angularVelocity.magnitude;
        Vector3 axis = ballRB.angularVelocity.normalized;
        Vector3 horiVelocity = ballRB.velocity;
        horiVelocity.y = 0;
        if (horiVelocity.magnitude > 0.01f)
        {
            horiVelocityDir = horiVelocity;
            horiVelocityDir.Normalize();
        }
        angle = Mathf.Lerp(0, angleMax, Mathf.InverseLerp(0, speedReachMaxAngle, angleSpeed));
        hamsterPivot.position = transform.position;
        hamsterPivot.localRotation = Quaternion.AngleAxis(-angle,axis)*
            Quaternion.LookRotation(horiVelocityDir,Vector3.up);

    }
}

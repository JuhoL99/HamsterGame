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
    void Start()
    {
        hamster = hamsterPivot.GetChild(0);
        ballRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float angleSpeed = ballRB.angularVelocity.magnitude;
        Vector3 axis = ballRB.angularVelocity.normalized;
        Vector3 moveDir = ballRB.velocity;
        moveDir.y = 0;
        moveDir.Normalize();
        angle = Mathf.Lerp(0, angleMax, Mathf.InverseLerp(0, speedReachMaxAngle, angleSpeed));
        hamsterPivot.position = transform.position;
        hamsterPivot.localRotation = Quaternion.AngleAxis(-angle,axis)*Quaternion.LookRotation(moveDir,Vector3.up);

    }
}

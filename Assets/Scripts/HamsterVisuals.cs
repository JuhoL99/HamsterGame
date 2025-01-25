using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class HamsterVisuals : MonoBehaviour
{
    public float angleMax, speedReachMaxAngle;
    public Transform hamsterPivot;
    Transform hamster;
    float angle;
    Rigidbody ballRB;
    bool spinning;
    Vector3 moveDir, moveAxis;
    Quaternion storedRotation;
    Quaternion targetRotation;
    void Start()
    {
        hamster = hamsterPivot.GetChild(0);
        ballRB = GetComponent<Rigidbody>();
        moveDir = Vector3.forward;
        moveAxis = Vector3.right;
    }
    
    void Update()
    {
        
        if (spinning) return;
        float angleSpeed = ballRB.angularVelocity.magnitude;
        Vector3 horiAngleSpeed = ballRB.angularVelocity.normalized;
        horiAngleSpeed.y = 0;
        if (horiAngleSpeed.magnitude > 0.01f)
        {
            moveAxis = horiAngleSpeed;
            moveAxis.Normalize();
            moveDir = Vector3.Cross(Vector3.up, moveAxis);
        }

        angle = Mathf.Lerp(0, angleMax, Mathf.InverseLerp(0, speedReachMaxAngle, angleSpeed));
        hamsterPivot.position = transform.position;
        targetRotation = Quaternion.AngleAxis(-angle,moveAxis)*
            Quaternion.LookRotation(moveDir,Vector3.up);
        storedRotation = hamsterPivot.rotation;
        hamsterPivot.localRotation = Quaternion.Slerp(hamsterPivot.rotation, targetRotation, 0.5f);
    }
    public void StartSpin()
    {
        spinning = true;
        hamsterPivot.SetParent(transform);
    }
    public void StopSpin()
    {
        spinning = false;
        hamsterPivot.SetParent(null);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;

public class HamsterVisuals : MonoBehaviour
{
    public float angleMax, speedReachMaxAngle;
    public Animator anim;
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
        Vector3 horiAngleVelocity = ballRB.angularVelocity;
        horiAngleVelocity.y = 0;
        float horiAngleSpeed = horiAngleVelocity.magnitude;
        if (spinning)
        {
            hamster.localScale = new Vector3(1, 1 - Mathf.Clamp(horiAngleSpeed * 0.1f, 0f, 0.8f), 1);
            if (horiAngleSpeed < 0.3) anim.SetBool("Spinning", false);
            return;
        }
        hamster.localScale = Vector3.one;
        if (horiAngleVelocity.magnitude > 0.01f)
        {
            moveAxis = horiAngleVelocity;
            moveAxis.Normalize();
            moveDir = Vector3.Cross(Vector3.up, moveAxis);
        }
        bool runnin = horiAngleSpeed > 0.3f;
        anim.SetBool("Runnin", runnin);
        if (runnin) anim.SetFloat("RunSpeed", horiAngleSpeed*0.5f);
        

        angle = Mathf.Lerp(0, angleMax, Mathf.InverseLerp(0, speedReachMaxAngle, horiAngleSpeed));
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
        anim.SetBool("Runnin", false);
        anim.SetBool("Spinning", true);
    }
    public void StopSpin()
    {
        spinning = false;
        hamsterPivot.SetParent(null);
        anim.SetBool("Spinning", false);

    }
}

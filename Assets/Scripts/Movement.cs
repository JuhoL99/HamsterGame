using Cinemachine;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CinemachineFreeLook freeLookCam;
    public float torque;
    Rigidbody rb;
    Camera cam;
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector2 inputDir = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

        Vector3 forward = cam.transform.forward;
        forward.y = 0;
        forward.Normalize();
        Vector3 right = Vector3.Cross(Vector3.up, forward);

        Vector3 moveDir = forward * inputDir.y + right * inputDir.x;
        Vector3 moveTorqueAxis = Vector3.Cross(Vector3.up, moveDir);
        rb.AddTorque(moveTorqueAxis*Time.fixedDeltaTime*torque);
    }
}

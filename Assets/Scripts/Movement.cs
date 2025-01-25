using Cinemachine;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CinemachineFreeLook freeLookCam;
    public Transform cameraFollow;
    public float torque;
    Rigidbody rb;
    Camera cam;
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }
    private void LateUpdate()
    {
        cameraFollow.position = transform.position;
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
        if (moveDir.magnitude > 0.01f) rb.AddTorque(moveTorqueAxis*Time.fixedDeltaTime*torque);
    }
}

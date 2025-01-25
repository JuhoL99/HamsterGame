using Cinemachine;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform cameraFollow;
    public float torque, loseControlAngleSpeed, regainControlAngleSpeed, controlLostTime;
    Rigidbody rb;
    Camera cam;
    HamsterVisuals hamsterVisuals;
    bool inControl = true;
    float controlLostTimer = 0f;
    Transform hamsterPivot;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        hamsterVisuals = GetComponent<HamsterVisuals>();
        hamsterPivot = hamsterVisuals.hamsterPivot;
    }
    private void LateUpdate()
    {
        cameraFollow.position = transform.position;
    }
    void EnableHamsterRB()
    {
       
    }
    private void Update()
    {
        if (controlLostTimer > 0f)
        {
            controlLostTimer -= Time.deltaTime;
        }

    }
    void FixedUpdate()
    {
        Vector2 inputDir = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        float angleSpeed = rb.angularVelocity.magnitude;
        
        if (inputDir.magnitude < 0.01f && angleSpeed > loseControlAngleSpeed && inControl)
        {
            print("LostControl");
            inControl = false;
            controlLostTimer = controlLostTime;
            hamsterVisuals.StartSpin();
        }
        /*
        print(controlLostTimer);
        print(inputDir.magnitude);
        print(inControl);
        print(angleSpeed);*/
        Vector3 rotationAngles = hamsterPivot.rotation.eulerAngles;
        if (controlLostTimer <= 0f && inputDir.magnitude > 0.01f && !inControl && (angleSpeed < regainControlAngleSpeed
            || Quaternion.Angle(Quaternion.Euler(rotationAngles.x, 0, rotationAngles.z), Quaternion.identity) < 45f))
        {
            print("RegainedControl");
            inControl = true;
            hamsterVisuals.StopSpin();
        }
        if (!inControl) return;
        Vector3 forward = cam.transform.forward;
        forward.y = 0;
        forward.Normalize();
        Vector3 right = Vector3.Cross(Vector3.up, forward);

        Vector3 moveDir = forward * inputDir.y + right * inputDir.x;
        Vector3 moveTorqueAxis = Vector3.Cross(Vector3.up, moveDir);
        if (moveDir.magnitude > 0.01f) rb.AddTorque(moveTorqueAxis*Time.fixedDeltaTime*torque);
    }
}

using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform cameraFollow;
    public float torque, loseControlAngleSpeed, regainControlAngleSpeed, controlLostTime, jumpForce;
    Rigidbody rb;
    Camera cam;
    HamsterVisuals hamsterVisuals;
    bool inControl = true;
    float controlLostTimer = 0f;
    Transform hamsterPivot;
    Vector3 origPos;
    private bool onGround;
    AudioSource jumpAudio;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        hamsterVisuals = GetComponent<HamsterVisuals>();
        hamsterPivot = hamsterVisuals.hamsterPivot;
        origPos = transform.position;

        AudioSource[] audioSources = GetComponents<AudioSource>();
        jumpAudio = audioSources[1];
    }
    private void LateUpdate()
    {
        cameraFollow.position = transform.position;
    }
    private void Update()
    {

        if (controlLostTimer > 0f)
        {
            controlLostTimer -= Time.deltaTime;
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, Vector3.down, 2.5f))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpAudio.Play();
        }

        //ResetPos
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = origPos;
        }
    }
    
    void FixedUpdate()
    {


        Vector2 inputDir = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        float angleSpeed = rb.angularVelocity.magnitude;
        
        if (inputDir.magnitude < 0.01f && angleSpeed > loseControlAngleSpeed && inControl)
        {
            inControl = false;
            controlLostTimer = controlLostTime;
            hamsterVisuals.StartSpin();
        }
        Vector3 rotationAngles = hamsterPivot.rotation.eulerAngles;
        if (controlLostTimer <= 0f && inputDir.magnitude > 0.01f && !inControl && (angleSpeed < regainControlAngleSpeed
            || Quaternion.Angle(Quaternion.Euler(rotationAngles.x, 0, rotationAngles.z), Quaternion.identity) < 45f))
        {
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

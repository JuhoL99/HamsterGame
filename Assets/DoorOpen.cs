using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    Transform hinge;
    bool open;
    Quaternion targetRotation;
    public void OpenDoor()
    {
        Debug.Log("Opened door");
        open = true;
    }
    void Start()
    {
        hinge = transform.parent;
        targetRotation = Quaternion.Euler(0, -45f, 0);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            OpenDoor();
        }
        if (open)
        {
            hinge.rotation = Quaternion.Slerp(hinge.rotation, targetRotation, 0.2f);
        }
    }
}

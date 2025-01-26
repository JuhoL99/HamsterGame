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
        open = true;
    }
    void Start()
    {
        hinge = transform.parent;
        targetRotation = Quaternion.Euler(0, -45f, 0);
    }

    void Update()
    {
        if (open)
        {
            hinge.rotation = Quaternion.Slerp(hinge.rotation, targetRotation, 0.2f);
        }
    }
}

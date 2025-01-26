using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    Transform cam;
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 objToCam = cam.position - transform.position;
        objToCam.y = 0;
        objToCam.Normalize();
        transform.rotation = Quaternion.LookRotation(objToCam);
    }
}

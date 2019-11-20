using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMovement : MonoBehaviour
{
    public Transform centerEye;
    public float speed = 1f;
    public float deadZoneThresh = 0.05f;
    public Transform rightHandController;
    Transform target;
    Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).magnitude > deadZoneThresh)
        {
            gameObject.transform.position += centerEye.forward * (Time.deltaTime * speed);
        }

        RaycastHit hit;

        if(Physics.Raycast(rightHandController.position, rightHandController.forward, out hit))
        {
            destination = hit;
        }

        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            transform.position = destination;
        }

    }
}

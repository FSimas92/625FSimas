using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    float camLookAhead = 6f;
    float camFollowBehind = 12f;
    float camFollowAbove = 8f;
    float rotateSpeed = 60f;
    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Vector3 cameraPosition = transform.position + (Vector3.up * camFollowAbove) + (-transform.forward * camFollowBehind);
        Camera.main.transform.position = cameraPosition;
        Camera.main.transform.LookAt(transform.position + transform.forward * camLookAhead);

        if (Input.GetKey(KeyCode.W))
        {
             cc.Move(transform.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            cc.Move(-transform.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            cc.Move(-transform.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            cc.Move(transform.right * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            Debug.Log("Player detected");
        }
    }
}

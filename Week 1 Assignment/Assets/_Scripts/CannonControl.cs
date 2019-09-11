using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControl : MonoBehaviour
{
    public GameObject piggyPlayer;
    public float strength = 500; //the scalar that defines the strength of the cannon launch
    Vector3 direction;
    const int MAX_ANGLE = 80;
    const int MIN_ANGLE = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);

        direction = mousePositionInWorld - transform.position;

        float alpha = Mathf.Acos(Vector3.Dot(Vector3.right, direction.normalized)) * Mathf.Rad2Deg;

        if (alpha < MAX_ANGLE && alpha > MIN_ANGLE && direction.y > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, alpha));
        }
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            piggyPlayer.transform.parent = null;
            piggyPlayer.GetComponent<Rigidbody2D>().gravityScale = 1;
            piggyPlayer.GetComponent<Rigidbody2D>().AddForce(direction * strength);
        }
    }
}

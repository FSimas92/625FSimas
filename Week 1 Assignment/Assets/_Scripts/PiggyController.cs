using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiggyController : MonoBehaviour
{
    Vector3 startPosition;
    Quaternion startRotation;
    Transform cannon;
    const int waitTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        cannon = transform.parent;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ScoreManager.instance.score++;

        StartCoroutine("ResetPiggy");
    }

    IEnumerator ResetPiggy()
    {
        yield return new WaitForSeconds(waitTime);
        transform.position = startPosition;
        transform.rotation = startRotation;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        transform.parent = cannon.transform;
    }
}

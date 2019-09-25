
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PiggyController : MonoBehaviour
{
    Vector3 startPosition;
    Quaternion startRotation;
    Transform cannon;
    public ScoreManager scoreManager;
    public LevelManager levelManager;
    const int WAIT_TIME = 3;
    bool resetting = false;
    public static int kingHits = 0;

    // Start is called before the first frame update
    void Start()
    {
        cannon = transform.parent;
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
        levelManager.updateLevel(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (kingHits >= 3)
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        scoreManager.updateScore(1);
        if (!resetting)
        {
            Invoke("ResetPiggy", 4);
            resetting = true;
        }

        if (collision.gameObject.tag == "King")
        {
            Debug.Log("hitKing");
            kingHits = kingHits + 1;
        }
    }

    void ResetPiggy()
    {
        transform.parent = cannon;
        transform.localPosition = startPosition;
        transform.localRotation = startRotation;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        levelManager.updateLevel(1);
        resetting = false;
    }
}

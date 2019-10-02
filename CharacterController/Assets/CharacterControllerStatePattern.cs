using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerStatePattern : MonoBehaviour
{
    enum PLAYER_STATE { S_WALK, S_IDLE, S_RUN, S_JUMP};
    PLAYER_STATE state;
    Animator anim;
    Rigidbody rb;

    public float jumpForce = 250;
    public float moveForce = 100;

    // Start is called before the first frame update
    void Start()
    {
        state = PLAYER_STATE.S_IDLE;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpForce);
            anim.SetTrigger("jump");
        }

        
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * moveForce);
        }
        


        switch (state)
        {
            case PLAYER_STATE.S_IDLE:
                if (Input.GetKeyDown(KeyCode.W))
                {
                    state = PLAYER_STATE.S_WALK;
                    anim.SetTrigger("walk");
                }
                break;

            case PLAYER_STATE.S_WALK:

                if (Input.GetKeyUp(KeyCode.W))
                {
                    state = PLAYER_STATE.S_IDLE;
                    anim.SetTrigger("stop");
                }
                
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    state = PLAYER_STATE.S_RUN;
                    anim.SetTrigger("run");
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    state = PLAYER_STATE.S_JUMP;
                    anim.SetTrigger("jump");
                }
                break;

            case PLAYER_STATE.S_RUN:

                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    state = PLAYER_STATE.S_WALK;
                    anim.SetTrigger("walk");
                }

                if (Input.GetKeyUp(KeyCode.W))
                {
                    state = PLAYER_STATE.S_IDLE;
                    anim.SetTrigger("stop");
                }

                break;

            case PLAYER_STATE.S_JUMP:

                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(state == PLAYER_STATE.S_JUMP)
        {
            state = PLAYER_STATE.S_IDLE;
            anim.SetTrigger("stop");
        }
    }

}

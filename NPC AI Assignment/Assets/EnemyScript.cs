using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject moveSpot;
    public GameObject moveSpot2;
    Transform player;

    enum NPC_STATES { Patrol, Chase };
    NPC_STATES state;

    Vector3 destination;

    RaycastHit hit;
    Vector3 rayDirection;

    // Start is called before the first frame update
    void Start()
    {
        state = NPC_STATES.Patrol;
        agent = gameObject.GetComponent<NavMeshAgent>();
        destination = moveSpot.transform.position;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        rayDirection = (player.transform.position - transform.position);
        bool raycastdown = Physics.Raycast(transform.position, rayDirection, out hit);

        if (raycastdown && hit.transform.name.Equals("Player"))
        {
            state = NPC_STATES.Chase;
        }
        else
        {
            state = NPC_STATES.Patrol;
        }

        switch (state)
        {
            case NPC_STATES.Chase:

                agent.SetDestination(player.position);

                break;

            case NPC_STATES.Patrol:

                agent.SetDestination(destination);
                if (Vector3.Distance(transform.position, destination) < 0.4f)
                {
                    Patrolling();
                }

                break;
        }
        Debug.Log(state);
    }

    void Patrolling()
    {
        if (Vector3.Distance(transform.position, moveSpot.transform.position) < 0.4f)
        {
            destination = moveSpot2.transform.position;
            Debug.Log("moving to 2");
        }
        else
        {
            destination = moveSpot.transform.position;
            Debug.Log("moving to 1");
        }
    }
}

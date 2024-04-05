using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAI : MonoBehaviour
{
    public Transform player, startPosition;
    public Transform[] patrolPoints;

    private NavMeshAgent agent;

    public bool idle = false;
    public bool patrolling = true;
    public bool chasingPlayer = false;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolling)
        {
            Patrol();
        }
        else if (idle)
        {
            ReturnToPatrol();
            if (!agent.hasPath)
            {
                idle = false;
                patrolling = true;
            }
        }
        else if (chasingPlayer)
        {
            StartCoroutine(ChasePlayer());
        }
    }

    private void Patrol()
    {
        if (agent.transform.position.y > startPosition.position.y && !agent.hasPath)
        {
            agent.SetDestination(patrolPoints[1].position);
        }
        else if (agent.transform.position.y < startPosition.position.y )
        {
            agent.SetDestination(patrolPoints[0].position);
        }
    }

    private IEnumerator ChasePlayer()
    {
        agent.SetDestination(player.position);
        yield return new WaitForSecondsRealtime(3f);
        chasingPlayer = false;
        idle = true;
    }

    private void ReturnToPatrol()
    {
        agent.SetDestination(startPosition.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            idle = false;
            patrolling = false;
            chasingPlayer = true;
        }
    }
}

using NavMeshPlus.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class PatrolAI : MonoBehaviour
{
    public Transform player, startPosition;
    public Transform[] patrolPoints;

    private NavMeshAgent agent;

    public bool idle = false;
    public bool patrolling = true;
    public bool chasingPlayer = false;

    public float chasePlayerTime;

    private bool chasingPlayerCoroutine = false;

    private float distance;

    private int pointIndex = 0;



    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolling)
        {
            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(patrolPoints[pointIndex].transform.position.y - transform.position.y, patrolPoints[pointIndex].transform.position.x - transform.position.x);
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotate Object
            this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 90);
            distance = Vector3.Distance(transform.position, patrolPoints[pointIndex].position);
            Patrol();
        }
        else if (idle)
        {
            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(startPosition.transform.position.y - transform.position.y, startPosition.transform.position.x - transform.position.x);
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotate Object
            this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 90);
            ReturnToPatrol();
            if (!agent.hasPath)
            {
                idle = false;
                patrolling = true;
            }
        }
        else if (chasingPlayer)
        {
            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x);
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotate Object
            this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 90);
            //transform.up = agent.destination - transform.position;
            agent.SetDestination(player.position);
        }
    }

    private void Patrol()
    {
        //if (Vector3.Distance(agent.transform.position, patrolPoints[1].position) < 1)
        //{
        //    Debug.Log("Moving up");
        //    agent.SetDestination(patrolPoints[0].position);
        //}
        //else if (Vector3.Distance(agent.transform.position, patrolPoints[0].position) < 1)
        //{
        //    Debug.Log("Moving down");
        //    agent.SetDestination(patrolPoints[1].position);
        //}
        //if (distance <= agent.stoppingDistance + 0.4f)
        //{
        //    if (pointIndex == 0)
        //    {
        //        pointIndex = 1;
        //        agent.SetDestination(patrolPoints[pointIndex].position);
        //    }
        //    else if (pointIndex == 1)
        //    {
        //        pointIndex = 0;
        //        agent.SetDestination(patrolPoints[pointIndex].position);
        //    }
        //}

        //if (agent.remainingDistance <= agent.stoppingDistance)
        //{
        //    pointIndex = (pointIndex + 1) % patrolPoints.Length;
        //    agent.SetDestination(patrolPoints[pointIndex].position);
        //}
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[pointIndex].position, 1 * Time.deltaTime);
        if (distance < 1.5f)
        {
            pointIndex = (pointIndex + 1) % patrolPoints.Length;
        }
    }

    private IEnumerator ChasePlayer()
    {
        chasingPlayerCoroutine = true;
        yield return new WaitForSecondsRealtime(chasePlayerTime);
        chasingPlayer = false;
        idle = true;
        chasingPlayerCoroutine = false;
    }

    private void ReturnToPatrol()
    {
        agent.SetDestination(startPosition.position);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject == player.gameObject)
        {
            idle = false;
            patrolling = false;
            chasingPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject == player.gameObject)
        {
            if (!chasingPlayerCoroutine)
            {
                StartCoroutine(ChasePlayer());
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public Transform player;
    public float FOVSize = 0.7f;
    public float chaseDistance = 5;

    int m_CurrentWaypointIndex;
    bool isChasing = false;

    public AudioSource spottedAudio;

    public TrailRenderer ghostTrail;

    void Start ()
    {
        navMeshAgent.SetDestination (waypoints[0].position);
    }

    void Update()
    {
        if (!isChasing)
        {
            Patrol();
            CheckForPlayer();
            if (ghostTrail.emitting)
            {
                ghostTrail.emitting = false;
            }
        }
        else
        {
            ChasePlayer();
            if (!ghostTrail.emitting)
            {
                ghostTrail.emitting = true;
            }
        }
    }

    void Patrol()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }
    void CheckForPlayer()
    {
        //Distance from ghost
        float distanceFromPlayer = Vector3.Distance(player.position, transform.position);
        //get player direction from ghost
        Vector3 directionFromPlayer = (player.position - transform.position).normalized;
        //where ghost is looking
        Vector3 ghostForward = transform.forward;
        //get dot product
        float dotProduct = Vector3.Dot(directionFromPlayer, ghostForward);
        //see if player is between the fov and the distance of the ghost
        if (dotProduct > FOVSize && distanceFromPlayer < chaseDistance)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionFromPlayer, out hit, chaseDistance))
            {
                if (hit.transform == player)
                {
                    SpottedSound();
                    isChasing = true;
                }
            }
        }
    }

    void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.position);

        float distance = Vector3.Distance(transform.position, player.position);

        float maxSpeed = 2.5f;
        float minSpeed = 0.5f;
        float maxChaseDistance = 7.5f;

        float t = Mathf.Clamp01(distance / maxChaseDistance);
        navMeshAgent.speed = Mathf.Lerp(minSpeed, maxSpeed, t);
    }

    void SpottedSound()
    {
        if (spottedAudio != null)
        {
            spottedAudio.Play();
        }
    }
}

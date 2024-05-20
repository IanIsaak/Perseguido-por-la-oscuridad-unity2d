using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rb;

    private enum State
    {
        Roaming,
        ChaseTarget
    }

    private State state;
    private Vector3 roamingPosition;
    public float roamRadius = 10f;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        state = State.Roaming;

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            switch (state)
            {
                case State.Roaming:
                    GenerateRoamingPosition();
                    seeker.StartPath(rb.position, roamingPosition, OnPathComplete);
                    break;
                case State.ChaseTarget:
                    seeker.StartPath(rb.position, target.position, OnPathComplete);
                    break;
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        FindTarget();

        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private void GenerateRoamingPosition()
    {
        roamingPosition = (Vector2)transform.position + Random.insideUnitCircle * roamRadius;
    }

    private void FindTarget()
    {
        float targetRange = 6f;
        if (Vector3.Distance(transform.position, target.position) < targetRange)
        {
            //Player within the range
            state = State.ChaseTarget;
        }
    }
}
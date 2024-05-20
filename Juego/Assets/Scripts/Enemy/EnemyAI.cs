using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private bool waiting = false;

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

        GenerateRoamingPosition();
        seeker.StartPath(rb.position, roamingPosition, OnPathComplete);
    }

    void FixedUpdate()
    {
        FindTarget();

        if (path == null || waiting)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            StartCoroutine(WaitAndGenerateNewPath());
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
            state = State.ChaseTarget;
            seeker.StartPath(rb.position, target.position, OnPathComplete);
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

    private IEnumerator WaitAndGenerateNewPath()
    {
        waiting = true;
        yield return new WaitForSeconds(2);
        GenerateRoamingPosition();
        seeker.StartPath(rb.position, roamingPosition, OnPathComplete);
        waiting = false;
    }
}

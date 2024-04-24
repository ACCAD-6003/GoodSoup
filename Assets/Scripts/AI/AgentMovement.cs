using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    public Waypoint destination; // Waypoints to move between
    private NavMeshAgent agent;
    private Transform destinationPos;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNextDestination();
    }

    void SetNextDestination()
    {
        destinationPos = this.destination.transform;
        Vector3 destination = destinationPos.position;

        agent.SetDestination(destination);
    }

    void Update()
    {
        Vector3 direction = (destinationPos.position - transform.position).normalized;
        float distanceToNextWaypoint = Vector3.Distance(transform.position, destinationPos.position);
        if (distanceToNextWaypoint > 0f)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
            {
                // Moving horizontally
                agent.Move(new Vector3(direction.x, 0, 0) * Time.deltaTime * agent.speed);
            }
            else
            {
                // Moving vertically
                agent.Move(new Vector3(0, 0, direction.z) * Time.deltaTime * agent.speed);
            }
        }
    }
}
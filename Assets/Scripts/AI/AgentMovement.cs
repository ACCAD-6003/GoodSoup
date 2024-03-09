using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    public Transform destination; // Waypoints to move between
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNextDestination();
    }

    void SetNextDestination()
    {
        Vector3 destination = this.destination.position;
        agent.SetDestination(destination);
    }

    void Update()
    {
        Vector3 direction = (destination.position - transform.position).normalized;
        float distanceToNextWaypoint = Vector3.Distance(transform.position, destination.position);
        if (distanceToNextWaypoint > agent.stoppingDistance)
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
        else {
            Debug.Log("Arrived");
        }
    }
}
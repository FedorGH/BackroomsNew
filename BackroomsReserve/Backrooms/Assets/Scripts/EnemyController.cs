using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    private int currentWaypointIndex = 0;
    public GameObject player;
    public float visionRange = 10f;
    public AudioClip Vizg;
    public AudioSource VizgS;
    public Animator animator;
    public bool isRun;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    void GotoNextPoint()
    {
        animator.SetTrigger("Stop");
        isRun = false;
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    public void Audio()
    {
        Debug.Log("Work");
        VizgS.PlayOneShot(Vizg);
    }
    public void AudioStop()
    {
        VizgS.Stop();
    }

    void Update()
    {
        if (isRun) Audio();
        if (!isRun) AudioStop();

        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < visionRange)
            {
                RaycastHit hit;
                Vector3 directionToPlayer = player.transform.position - transform.position;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit))
                {
                    if (hit.collider.gameObject == player)
                    {
                        navMeshAgent.SetDestination(player.transform.position);
                        animator.SetTrigger("Run");
                        isRun = true;
                        navMeshAgent.GetComponent<NavMeshAgent>().speed = 9f;
                        return;
                    }
                    if (hit.collider.gameObject != player)
                    {
                        isRun = false;
                        navMeshAgent.SetDestination(player.transform.position);
                        animator.SetTrigger("Stop");
                        return;
                    }

                }
            }
        }
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < visionRange)
            {
                RaycastHit hit;
                Vector3 directionToPlayer = player.transform.position - transform.position;
                
                if (Physics.Raycast(transform.position, directionToPlayer, out hit))
                {
                    if (hit.collider.gameObject == player)
                    {
                        Debug.Log("Collider");
                        navMeshAgent.GetComponent<NavMeshAgent>().speed = 9f;
                        navMeshAgent.SetDestination(player.transform.position);
                        isRun = true;
                        return;
                    }
                }
            }
        }
    }
}


using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] points;
    public GameObject player;
    public float visionRange = 10f;
    public AudioClip Vizg;
    public AudioSource VizgS;
    public Animator animator;
    public float walkSpeed;
    public float speedRun;

    private int currentWaypointIndex = 0;
    private bool isRun = false;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.autoBraking = false;
        GotoNextPoint();
    }

    private void GotoNextPoint()
    {
        animator.SetTrigger("Stop");
        isRun = false;

        if (points.Length == 0)
            return;

        navMeshAgent.destination = points[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % points.Length;
    }

    private void Audio(bool play)
    {
        if (play)
            VizgS.PlayOneShot(Vizg);
        else
            VizgS.Stop();
    }

    private void Update()
    {
        bool previousRunState = isRun;
        isRun = false;

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < visionRange)
            {
                RaycastHit hit;
                Vector3 directionToPlayer = player.transform.position - transform.position;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit) && hit.collider.gameObject == player)
                {
                    navMeshAgent.speed = speedRun;
                    navMeshAgent.SetDestination(player.transform.position);
                    animator.SetTrigger("Run");
                    isRun = true;
                }
                else
                {
                    navMeshAgent.speed = walkSpeed;
                    navMeshAgent.SetDestination(player.transform.position);
                    animator.SetTrigger("Stop");
                }
            }
        }

        if (isRun != previousRunState)
            Audio(isRun);

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}
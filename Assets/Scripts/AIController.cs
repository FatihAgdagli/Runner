using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIController : Controller
{
    [SerializeField]
    private GameObject destination;
    private NavMeshAgent agent;
    private bool isAiStarted = false;
    private Vector3 startPosition;
    private bool willBeRespown;
    private float aiControlRange = 5f;

    override protected void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        base.Awake();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Alive && other.gameObject.CompareTag("Obstacle"))
        {
            Alive = false;           
        }
    }
    override protected void Update()
    {
        base.Update();
        agent.speed = speed;

        if(transform.position.z < destination.transform.position.z)
            SetDestinationForAI();
        else
            ControlbySimpleAI();

        if(!Alive && !willBeRespown)
        {
            willBeRespown = true;
            StartCoroutine(Respawn());
        }
    }

    private void SetDestinationForAI()
    {
        //if (!agent.enabled) agent.enabled = true;

        if (!isAiStarted && GameManager.Instance.Started)
        {
            agent.SetDestination(destination.transform.position);
            isAiStarted = true;
        }

        if ((!Alive || GameManager.Instance.LevelFinished))
        {
            agent.ResetPath();
        }
    }
    private void ControlbySimpleAI()
    {
        //if (agent.enabled) agent.enabled = false;
        agent.ResetPath();

        if (Alive && !GameManager.Instance.LevelFinished)
        {
            MoveSimpleAI();
        }
        else if (GameManager.Instance.LevelFinished)
        {
            rb.velocity = Vector3.zero;
        }
    }
    private void MoveSimpleAI()
    {
        float movementX = 0f;
        float movementZ = 1f;

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            if (hit.distance < aiControlRange)
            {
                movementX = Random.Range(-1f, 1f);
            }
        }

        Vector3 movement = new Vector3(movementX, 0, movementZ);
        if (onRotatingPlatform) movement -= Vector3.right * rotatingPlatformEfect;
        movement *= speed;
        movement = Vector3.ClampMagnitude(movement, speed);

        if (isGrounded) rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1.5f);
        agent.Warp(startPosition);
        Alive = true;
        willBeRespown = false;
        agent.ResetPath();
        StartCoroutine(StartAfterRespawn());
    }
    IEnumerator StartAfterRespawn()
    {
        yield return new WaitForSeconds(.5f);
        agent.SetDestination(destination.transform.position);
    }
}

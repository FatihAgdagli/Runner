using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Controller : MonoBehaviour
{
    [SerializeField] protected float speed = 5;
    protected AnimationHandler animationHandler;  
    protected Rigidbody rb;

    protected bool isGrounded = true;
    protected bool onRotatingPlatform = false;
    protected float rotatingPlatformEfect;

    public bool Alive { get; protected set; }
    public bool LevelFinished { get; protected set; }

    virtual protected void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
        rb = GetComponent<Rigidbody>();
        Alive = true;
        LevelFinished = false;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        PlayAnimation();
    }

    virtual protected void FixedUpdate()
    {
        CheckGround();
    }

    virtual protected void OnCollisionEnter(Collision collision)
    {
        //Debug.Log($"{gameObject.name} collided with {collision.gameObject.name}" );
        if (Alive && collision.gameObject.CompareTag("Obstacle"))
        {   
            Alive = false;
        }
    }

    private void CheckGround()
    {
        onRotatingPlatform = false;

        float distToSurface = GetComponent<Collider>().bounds.extents.y;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down,out hit, (distToSurface - 0.1f)))
        {
            isGrounded = true;

            if (hit.transform.gameObject.CompareTag("MovablePlatform"))
                onRotatingPlatform = true;
            else if (hit.transform.gameObject.CompareTag("Finish"))
                LevelFinished = true;         
        }
        else
        {
            isGrounded = false;
        }

        if (!isGrounded && transform.position.y < -3f) Alive = false;
    }
    virtual protected void PlayAnimation()
    {
        if (!Alive)
        {
            if(isGrounded) animationHandler.GoCollide();
        }
        else if (GameManager.Instance.LevelFinished)
        {
            if (LevelFinished) animationHandler.GoVictory();
            else animationHandler.GoCollide();
        }
        else
        {
            if (isGrounded)
            {
                if (GameManager.Instance.Started) animationHandler.GoRun();
                else animationHandler.GoIdle();
            }
            else
            {
                animationHandler.GoFall();
            }
        }

    }

    // Called by Rotation Platform 
    public void AddForce(float force) => rotatingPlatformEfect = Mathf.Clamp(force, -0.5f, 0.5f);
}

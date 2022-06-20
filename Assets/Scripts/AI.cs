using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] float speed = 3.0f;
    [SerializeField] float obstacleRange = 5.0f;

    public bool IsAlive { get; set; }

    private float movementX;
    private float movementZ;


    private void Start()
    {
        IsAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAlive)
        {
            movementZ = 1f;

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                if (hit.distance < obstacleRange)
                {
                    movementX = Random.Range(-1f, 1f);
                }
            }
        }
        else
        {
            movementX = 0f;
            movementZ = 0f;
        }
    }

    public float GetHorizontalInput()
    {    
        return movementX;
    }

    public float GetVerticalInput()
    {
        return movementZ;
    }
}

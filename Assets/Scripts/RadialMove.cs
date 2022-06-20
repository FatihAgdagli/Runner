using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Axis
{
    X,
    Y,
    Z,
}

public class RadialMove : MonoBehaviour
{
    [SerializeField] protected bool clockWise = false;
    [SerializeField] protected float speed = 100f;
    [SerializeField] protected Axis rotationAxis;

    protected Vector3 axis;

    // Start is called before the first frame update
    void Start()
    {
        switch(rotationAxis)
        {
            case Axis.X: axis = Vector3.right; break;
            case Axis.Y: axis = Vector3.up; break;
            case Axis.Z: axis = Vector3.forward; break;
            default: axis = Vector3.up; break;
        }

        if (!clockWise) axis *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    protected virtual void Rotate()
    {
        transform.Rotate(axis, speed*Time.deltaTime,Space.World);
    }
}

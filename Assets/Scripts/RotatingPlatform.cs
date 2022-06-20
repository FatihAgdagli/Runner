using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : RadialMove
{
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (rotationAxis == Axis.Z)
            {
                Vector3 rotationForce = Vector3.right;
                if (!clockWise) rotationForce *= -1;

                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                //player.AddForce(rotationForce * speed);
                if (!clockWise) player.AddForce(-speed);
                else player.AddForce(speed);
            }
        }
    }
}

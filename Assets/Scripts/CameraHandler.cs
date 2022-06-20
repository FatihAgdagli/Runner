using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] Transform targetToFollow;
    [SerializeField] Vector3 followOffset;
    [Space]
    [SerializeField] Transform FPSTransform;
    [SerializeField] Vector3 FPSOffset;

    private bool isCameraMoving = false;
    private bool FPSMode = false;
    private PlayerController player;

    void Awake()
    {
        player = targetToFollow.gameObject.GetComponent<PlayerController>();
    }

    private void LateUpdate()
    {
        if (FPSTransform != null)
        {
            if (player.LevelFinished)
            {
                if (!FPSMode)
                    StartCoroutine(WaitSomeTime());
            }
            else
            {
                FollowTheTarget();
            }

            GoFPSPosition();
        }
        else
        {
            FollowTheTarget();
        }
    }

    IEnumerator WaitSomeTime()
    {
        FPSMode = true;
        yield return new WaitForSeconds(3f);
        isCameraMoving = true;
    }
    private void GoFPSPosition()
    {
        Vector3 fpsPosition = FPSTransform.position + FPSOffset;

        if (isCameraMoving && 
            transform.position.y != fpsPosition.y - 0.1f &&
            transform.position.z != fpsPosition.z - 0.1f)
        {
            float posY = Mathf.Lerp(transform.position.y, fpsPosition.y, Time.deltaTime);
            float posZ = Mathf.Lerp(transform.position.z, fpsPosition.z, Time.deltaTime);

            transform.position = new Vector3(0, posY, posZ);
            transform.rotation = new Quaternion(0,0,0,0);

            Camera camera = Camera.main;
            camera.orthographic = true;

        }

        if (transform.position.y >= fpsPosition.y - 0.1f &&
            transform.position.z >= fpsPosition.z - 0.1f)
        {

            isCameraMoving=false;
            GameManager.Instance.EnableDrawing();
        }
    }

    private void FollowTheTarget()
    {

        transform.position = targetToFollow.position + followOffset;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMove : MonoBehaviour
{
    [SerializeField] bool goLeft = false;
    [SerializeField] float speed = 2f;
    [SerializeField] float xRange = 6f;
    [SerializeField] float watingTime = 1f;
    

    private Vector3 movement = Vector3.right;
    public float startPosX;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        FindDirection();
    }

    // Update is called once per frame
    void Update()
    {
        Travel();         
    }

    private void Travel()
    {
        float currentDeltaX = FindTravelledDistance();
        if (currentDeltaX < xRange)
        {
            isMoving = true;
            transform.Translate(movement * speed * Time.deltaTime);
        }
        else
        {
            if(isMoving) StartCoroutine(StaySomeTime());
        }       
    }

    private float FindTravelledDistance()
    {
        if (goLeft)
            return startPosX - transform.localPosition.x;
        else
            return transform.localPosition.x - startPosX;
    }

    IEnumerator StaySomeTime()

    {
        isMoving = false;    
        yield return new WaitForSeconds(watingTime);
        
        goLeft = !goLeft;
        FindDirection();
    }

    private void FindDirection()
    {
        startPosX = transform.localPosition.x;
        if (goLeft) movement *= -1;
        else movement = Vector3.right;
    }
}
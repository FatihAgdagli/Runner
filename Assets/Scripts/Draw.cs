using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    [SerializeField] private GameObject brush;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject obstacles;

    private Camera camera;
    private LineRenderer currentLineRenderer;
    private Vector2 lastPos;   

    private void Awake()
    {
        camera = Camera.main;
        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, -0.312f);
        wall.transform.position = new Vector3(wall.transform.position.x, wall.transform.position.y, 0.6f);

        platform.SetActive(false);
        obstacles.SetActive(false);
    }

    private void Update()
    {
        Drawing();
    }

    void Drawing()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateBrush();
            }
            else if (Input.GetMouseButton(0))
            {
                PointToMousePos();
            }
            else
            {
                currentLineRenderer = null;
            }
        }
    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        //because you gotta have 2 points to start a line renderer, 
        Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

    }

    void AddAPoint(Vector3 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    void PointToMousePos()
    {
        Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        if (lastPos != mousePos)
        {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
    }
}

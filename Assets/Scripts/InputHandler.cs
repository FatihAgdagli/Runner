using UnityEngine;

public class InputHandler : MonoBehaviour
{
    const string AXIS_HORIZONTAL = "Horizontal";
    const string AXIS_VERTICAL = "Vertical";

    [SerializeField] private float swerveSpeed = 0.5f;
    private float swerveDeltaX;
    private float lastMousePositionX;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            lastMousePositionX = Input.mousePosition.x;
        }
        else if(Input.GetMouseButton(0))
        {
            swerveDeltaX = Input.mousePosition.x - lastMousePositionX;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            swerveDeltaX = 0f;
        }
    }

    public float GetHorizontalInput()
    {
        float swerveAmount = swerveSpeed * swerveDeltaX;
        return Mathf.Clamp(swerveAmount, -1f, 1f);
    }

    public float GetVerticalInput()
    {
        return Input.GetAxis(AXIS_VERTICAL);
    }

}

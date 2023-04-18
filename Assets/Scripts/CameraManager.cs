using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteAlways]
public class CameraManager : MonoBehaviour
{
    #region Variables

    Vector2 mouseDelta = new Vector2();
    bool mouseIsDown = false;

    public float movementSpeed = 10.0f;

    public Vector3 horizontalAxis = new Vector3();
    public Vector3 verticalAxis = new Vector3();

    Transform rig;

    public Transform scrollStart;
    public Transform scrollEnd;
    [Range(0, 1)] public float scrollPosition = 0;

    new Camera camera;

    #endregion

    public bool ScrollIsValid() 
    {
        return scrollStart != null && scrollEnd != null;
    }

    public Vector3 GetScrollDirection() 
    {
        return (scrollEnd.position - scrollStart.position).normalized;
    }

    private void Start() 
    {
        rig = transform.Find("Rig");
        camera = GetComponentInChildren<Camera>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        mouseIsDown = context.started || context.performed;
    }

    private void Update() 
    {
        if (rig) 
        {
            var q = Quaternion.Euler(-rig.transform.localEulerAngles.x, 0, 0);
            verticalAxis = rig.transform.TransformVector(q * Vector3.forward);
        }     
        if (camera) 
        {
            horizontalAxis = camera.transform.right;
        }
        if (ScrollIsValid())
        {
            transform.position = Vector3.Lerp(scrollStart.position, scrollEnd.position, scrollPosition);
        }
    }

    private void LateUpdate()
    {
        if (mouseIsDown && ScrollIsValid())
        {
            float screenSize = (Screen.width + Screen.width) / 2f;
            Vector3 userDrag = 
                camera.transform.right * mouseDelta.x / screenSize
                + camera.transform.up * mouseDelta.y / screenSize;

            float progression = -Vector3.Dot(GetScrollDirection(), userDrag);
            scrollPosition = Mathf.Clamp01(scrollPosition + progression);
       }
    }

    private void OnDrawGizmos() 
    {
        if (camera)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(camera.transform.position, transform.position);
            Gizmos.DrawSphere(transform.position, 0.2f);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, horizontalAxis);
            Gizmos.DrawSphere(transform.position + horizontalAxis, 0.1f);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, verticalAxis);
            Gizmos.DrawSphere(transform.position + verticalAxis, 0.1f);
        }

        if (ScrollIsValid())
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(scrollStart.position, scrollEnd.position);
        }
    }
}
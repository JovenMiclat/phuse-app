using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbital : MonoBehaviour
{
    public Camera sceneCamera;
    public Transform target;

    [Range(5f, 15f)]
    [Tooltip("How sensitive the mouse drag to camera rotation")]
    public float mouseRotateSpeed = 5f;
    [Range(1f, 50f)]
    [Tooltip("How sensitive the touch drag to camera rotation")]
    public float touchRotateSpeed = 2f;
    [Tooltip("Smaller positive value means smoother rotation, 1 means no smooth apply")]
    public float slerpSmoothValue = 0.3f;
    [Tooltip("How long the smoothDamp of the mouse scroll takes")]
    public float scrollSmoothTime = 0.12f;
    public float editorFOVSensitivity = 5f;
    public float touchFOVSensitivity = 5f;
    private bool canRotate = true;
    private Vector2 swipeDirection;
    private Vector2 touch1OldPos;
    private Vector2 touch2OldPos;
    private Vector2 touch1CurrentPos;
    private Vector2 touch2CurrentPos;
    private Quaternion currentRot;
    private Quaternion targetRot;

    private Touch touch;
    private float rotX;
    private float rotY;

    private float cameraFieldOfView;
    private float cameraFOVDamp;
    private float fovChangeVelocity = 0;
    private float distanceBetweenCameraAndTarget;

    private float minXRotAngle = -85; 
    private float maxXRotAngle = 85; 
    private float minCameraFieldOfView = 20;
    private float maxCameraFieldOfView = 100;
    Vector3 dir;
    private void Awake()
    {
        GetCameraReference();
    }
    void Start()
    {
        distanceBetweenCameraAndTarget = Vector3.Distance(sceneCamera.transform.position, target.position);
        dir = new Vector3(0, 0, distanceBetweenCameraAndTarget);
        sceneCamera.transform.position = target.position + dir;
        cameraFOVDamp = sceneCamera.fieldOfView;
        cameraFieldOfView = sceneCamera.fieldOfView;

        TopView();
    }

    void Update()
    {
        if (!canRotate)
        {
            return;
        }
        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            EditorCameraInput();
        }
        else
        {
            TouchCameraInput();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            FrontView();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            TopView();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LeftView();
        }
    }
    private void LateUpdate()
    {
        RotateCamera();
        SetCameraFOV();
    }
    public void GetCameraReference()
    {
        if (sceneCamera == null)
        {
            sceneCamera = Camera.main;
        }
    }
    public void TopView()
    {
        rotX = -85;
        rotY = 0;
    }
    public void TopRightView()
    {
        rotX = -85;
        rotY = -90;
    }
    public void TopLeftView()
    {
        rotX = -80;
        rotY = 90;
    }
    public void LeftView()
    {
        rotY = 90;
        rotX = 0;
    }
    public void FrontView()
    {
        rotX = 0;
        rotY = 0;
    }
    private void EditorCameraInput()
    {

        if (Input.GetMouseButton(0))
        {
            rotX += Input.GetAxis("Mouse Y") * mouseRotateSpeed; 
            rotY += Input.GetAxis("Mouse X") * mouseRotateSpeed;
            if (rotX < minXRotAngle)
            {
                rotX = minXRotAngle;
            }
            else if (rotX > maxXRotAngle)
            {
                rotX = maxXRotAngle;
            }
        }
        if (Input.mouseScrollDelta.magnitude > 0)
        {
            cameraFieldOfView += Input.mouseScrollDelta.y * editorFOVSensitivity * -1;
        }
    }
    private void TouchCameraInput()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 1)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    swipeDirection += -touch.deltaPosition * touchRotateSpeed; //-1 make rotate direction natural
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                }
            }
            else if (Input.touchCount == 2)
            {
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);
                if (touch1.phase == TouchPhase.Began && touch2.phase == TouchPhase.Began)
                {
                    touch1OldPos = touch1.position;
                    touch2OldPos = touch2.position;
                }
                if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
                {
                    touch1CurrentPos = touch1.position;
                    touch2CurrentPos = touch2.position;
                    float deltaDistance = Vector2.Distance(touch1CurrentPos, touch2CurrentPos) - Vector2.Distance(touch1OldPos, touch2OldPos);
                    cameraFieldOfView += deltaDistance * -1 * touchFOVSensitivity;
                    touch1OldPos = touch1CurrentPos;
                    touch2OldPos = touch2CurrentPos;
                }
            }
        }
        if (swipeDirection.y < minXRotAngle)
        {
            swipeDirection.y = minXRotAngle;
        }
        else if (swipeDirection.y > maxXRotAngle)
        {
            swipeDirection.y = maxXRotAngle;
        }
    }
    private void RotateCamera()
    {
        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Vector3 tempV = new Vector3(rotX, rotY, 0);
            targetRot = Quaternion.Euler(tempV);
        }
        else
        {
            targetRot = Quaternion.Euler(-swipeDirection.y, swipeDirection.x, 0);
        }
        currentRot = Quaternion.Slerp(currentRot, targetRot, Time.smoothDeltaTime * slerpSmoothValue * 50);

        sceneCamera.transform.position = target.position + currentRot * dir;
        sceneCamera.transform.LookAt(target.position);
    }
    void SetCameraFOV()
    {
        if (cameraFieldOfView <= minCameraFieldOfView)
        {
            cameraFieldOfView = minCameraFieldOfView;
        }
        else if (cameraFieldOfView >= maxCameraFieldOfView)
        {
            cameraFieldOfView = maxCameraFieldOfView;
        }
        cameraFOVDamp = Mathf.SmoothDamp(cameraFOVDamp, cameraFieldOfView, ref fovChangeVelocity, scrollSmoothTime);
        sceneCamera.fieldOfView = cameraFOVDamp;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement Setup")]
    public float moveSpeed;
    public float zoomSpeed;

    [Header("Rotation Setup")]
    public float cameraSensitivity = 90f;
    [SerializeField]private float rotationX = 0.0f;
    [SerializeField]private float rotationY = 0.0f;

    [Header("Zoom Setup")]
    public float minZoomDistance;
    public float maxZoomDistance;

    [Header("Focus Setup")]
    public Transform focusTarget;
    public float focusSpeed;
    [SerializeField] bool focusing = false;

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        Move();
        Rotate();
        Zoom();
        Focus();
    }

    // Moves the camera with WASD
    void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 direction = transform.forward * inputZ + transform.right * inputX;

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    // Rotates the camera by holding left click and moving mouse
    void Rotate()
    {
        if (Input.GetButton("Fire2"))
        {
            rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, 0, 0);

            transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Zooms the camera by scrolling up and down with the scroll wheel
    void Zoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float distance = Vector3.Distance(transform.position, mainCam.transform.position);

        if (distance < minZoomDistance && scrollInput > 0.0f)
        {
            return;
        }

        else if (distance > maxZoomDistance && scrollInput < 0.0f)
        {
            return;
        }

        mainCam.transform.position += mainCam.transform.forward * scrollInput * zoomSpeed;
    }

    // Refocuses the camera back on the squad
    void Focus()
    {
        if (Input.GetKeyDown(KeyCode.F) && focusing == false)
        {
            focusing = true;
        }

        if (focusing)
        {
            while (transform.position != focusTarget.position)
            {
                transform.position = Vector3.Lerp(transform.position, focusTarget.position, focusSpeed * Time.deltaTime);
            }
        }

        if (transform.position == focusTarget.position)
        {
            focusing = false;
        }
    }
}

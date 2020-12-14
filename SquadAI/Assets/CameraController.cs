using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement Setup")]
    public float moveSpeed;
    public float zoomSpeed;
    public float minZoomDistance;
    public float maxZoomDistance;

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        Move();
        Zoom();
    }

    void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 direction = transform.forward * inputZ + transform.right * inputX;

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPurchaseCanvas : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform); // Keeps the healthbar rotated towards the camera
        transform.Rotate(0, 180, 0);
    }
}

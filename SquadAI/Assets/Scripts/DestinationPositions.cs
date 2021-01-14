using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationPositions : MonoBehaviour
{
    [Header("Position Vectors")]
    public Vector3 gridPosition;
    public Vector3 linePosisition;

    private void Start()
    {
        transform.localPosition = gridPosition;
    }

    private void Update()
    {
        ChangeFormation();
    }

    // Functionality for changing formations
    void ChangeFormation()
    {
        // Position in the grid formation
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.localPosition = gridPosition;
        }

        // Position in the line formation
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.localPosition = linePosisition;
        }
    }
}

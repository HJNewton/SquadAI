using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationPositions : MonoBehaviour
{
    [Header("Position Vectors")]
    public Vector3 gridPosition;
    public Vector3 linePosisition;
    public Vector3 vShapePosition;

    SquadManager squadManager;

    private void Awake()
    {
        squadManager = GameObject.FindGameObjectWithTag("Squad Manager").GetComponent<SquadManager>();
    }

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
        if (Input.GetKeyDown(KeyCode.Alpha1) || squadManager.formation == SquadManager.Formation.Grid)
        {
            transform.localPosition = gridPosition;
            squadManager.formation = SquadManager.Formation.Grid;
        }

        // Position in the line formation
        if (Input.GetKeyDown(KeyCode.Alpha2) || squadManager.formation == SquadManager.Formation.Line)
        {
            transform.localPosition = linePosisition;
            squadManager.formation = SquadManager.Formation.Line;
        }

        // Position in the v shape formation
        if (Input.GetKeyDown(KeyCode.Alpha3) || squadManager.formation == SquadManager.Formation.VShape)
        {
            transform.localPosition = vShapePosition;
            squadManager.formation = SquadManager.Formation.VShape;
        }
    }
}

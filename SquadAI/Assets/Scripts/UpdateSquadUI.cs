using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSquadUI : MonoBehaviour
{
    [Header("UI Setup")]
    public Image gridHighlight;
    public Image lineHighlight;

    SquadManager squadManager;

    private void Awake()
    {
        squadManager = this.GetComponent<SquadManager>();
    }

    private void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        // Grid formation
        if (squadManager.formation == SquadManager.Formation.Grid)
        {
            gridHighlight.enabled = true;

            lineHighlight.enabled = false;
        }

        // Line formation
        if (squadManager.formation == SquadManager.Formation.Line)
        {
            lineHighlight.enabled = true;

            gridHighlight.enabled = false;
        }
    }
}
